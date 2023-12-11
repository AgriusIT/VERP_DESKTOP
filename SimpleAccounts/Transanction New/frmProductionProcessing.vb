Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility

Public Class frmProductionProcessing
    Implements IGeneral
    Dim _WIPDocId As Integer = 0I
    Dim WIPMod As WIPProductionMasterBE
    Dim IsOpenForm As Boolean = False
    Dim blnPOP As Boolean = False
    Dim StockList As List(Of StockDetail)
    Enum enmGrdDetail
        LocationId
        ArticleId
        PackQty
        Qty
        TotalQty
        Rate
        TotalAmount
        Comments
        VehicleNo
        GatepassNo
        TruckNo
        TransType
        SalesAccountId ''
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If (Me.UltraTabControl1.SelectedTab.Index = 0) Then
                For c As Integer = 0 To Me.grdGatePass.RootTable.Columns.Count - 1
                    If Me.grdGatePass.RootTable.Columns(c).Index <> enmGrdDetail.LocationId AndAlso Me.grdGatePass.RootTable.Columns(c).Index <> enmGrdDetail.PackQty AndAlso Me.grdGatePass.RootTable.Columns(c).Index <> enmGrdDetail.Qty AndAlso Me.grdGatePass.RootTable.Columns(c).Index <> enmGrdDetail.TotalQty AndAlso Me.grdGatePass.RootTable.Columns(c).Index <> enmGrdDetail.Rate AndAlso Me.grdGatePass.RootTable.Columns(c).Index <> enmGrdDetail.TotalAmount AndAlso Me.grdGatePass.RootTable.Columns(c).Index <> enmGrdDetail.Comments AndAlso Me.grdGatePass.RootTable.Columns(c).Index <> enmGrdDetail.TruckNo AndAlso Me.grdGatePass.RootTable.Columns(c).Index <> enmGrdDetail.VehicleNo Then
                        Me.grdGatePass.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
            ElseIf (Me.UltraTabControl1.SelectedTab.Index = 1) Then
                For c As Integer = 0 To Me.grdGatePass.RootTable.Columns.Count - 1
                    If Me.grdWIP.RootTable.Columns(c).Index <> enmGrdDetail.LocationId AndAlso Me.grdWIP.RootTable.Columns(c).Index <> enmGrdDetail.PackQty AndAlso Me.grdWIP.RootTable.Columns(c).Index <> enmGrdDetail.Qty AndAlso Me.grdWIP.RootTable.Columns(c).Index <> enmGrdDetail.TotalQty AndAlso Me.grdWIP.RootTable.Columns(c).Index <> enmGrdDetail.Rate AndAlso Me.grdWIP.RootTable.Columns(c).Index <> enmGrdDetail.TotalAmount AndAlso Me.grdWIP.RootTable.Columns(c).Index <> enmGrdDetail.Comments AndAlso Me.grdWIP.RootTable.Columns(c).Index <> enmGrdDetail.TruckNo AndAlso Me.grdWIP.RootTable.Columns(c).Index <> enmGrdDetail.VehicleNo Then
                        Me.grdWIP.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
            ElseIf (Me.UltraTabControl1.SelectedTab.Index = 2) Then
                For c As Integer = 0 To Me.grdGatePass.RootTable.Columns.Count - 1
                    If Me.grdProduction.RootTable.Columns(c).Index <> enmGrdDetail.LocationId AndAlso Me.grdProduction.RootTable.Columns(c).Index <> enmGrdDetail.PackQty AndAlso Me.grdProduction.RootTable.Columns(c).Index <> enmGrdDetail.Qty AndAlso Me.grdProduction.RootTable.Columns(c).Index <> enmGrdDetail.TotalQty AndAlso Me.grdProduction.RootTable.Columns(c).Index <> enmGrdDetail.Rate AndAlso Me.grdProduction.RootTable.Columns(c).Index <> enmGrdDetail.TotalAmount AndAlso Me.grdProduction.RootTable.Columns(c).Index <> enmGrdDetail.Comments AndAlso Me.grdProduction.RootTable.Columns(c).Index <> enmGrdDetail.TruckNo AndAlso Me.grdProduction.RootTable.Columns(c).Index <> enmGrdDetail.VehicleNo Then
                        Me.grdProduction.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            WIPMod = New WIPProductionMasterBE
            WIPMod.DocId = Val(Me.grdProductionHistory.GetRow.Cells("DocId").Value.ToString)
            WIPMod.LotNo = Me.grdProductionHistory.GetRow.Cells("LotNo").Value.ToString
            WIPMod.DocDate = Me.grdProductionHistory.GetRow.Cells("DocDate").Value
            '
            WIPMod.DocNo = Me.grdProductionHistory.GetRow.Cells("DocNo").Value.ToString
            '
            WIPMod.Voucher = New VouchersMaster
            WIPMod.Voucher.VNo = Me.grdProductionHistory.GetRow.Cells("DocNo").Value.ToString
            WIPMod.Voucher.VoucherNo = Me.grdProductionHistory.GetRow.Cells("DocNo").Value.ToString
            WIPMod.Voucher.VoucherCode = Me.grdProductionHistory.GetRow.Cells("DocNo").Value.ToString
            WIPMod.Voucher.VoucherDate = Me.grdProductionHistory.GetRow.Cells("DocDate").Value.ToString

            If New WIPProductionDAL().Delete(WIPMod) Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = String.Empty Then
                FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title as [Customer], detail_code as [Account Code], Sub_Sub_Title as [Account Head], Account_Type as [Account Type], Contact_Mobile as [Mobile], Contact_Email as [Email], CityName as City From vwCOADetail WHERE Detail_Title <> '' Order By Detail_Title")
                Me.cmbVendor.Rows(0).Activate()
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            ElseIf Condition = "PO" Then
                'FillDropDown(Me.cmbInvoice, "Select ReceivingMasterTable.ReceivingID, ReceivingMasterTable.ReceivingNo From ReceivingMasterTable LEFT OUTER JOIN(SELECT  ISNULL(wIPD.RefReceivingID, 0) AS RefReceivingId, SUM(WIP.TotalQty) AS WIPQty FROM   dbo.WIPProductionDetailTable AS WIP INNER JOIN  dbo.WIPProductionMasterTable AS wIPD ON WIP.DocId = wIPD.DocId WHERE DocId <> " & _WIPDocId & " GROUP BY ISNULL(wIPD.RefReceivingID, 0)) Wip On Wip.RefReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN (Select ReceivingId, SUM(Qty) as RecQty From ReceivingDetailTable Group By ReceivingId) RecvD on RecvD.ReceivingId = ReceivingMasterTable.ReceivingId WHERE (isnull(RecQty,0)-IsNull(WipQty,0)) > 0 AND VendorID=" & Me.cmbVendor.Value & "")
                'FillDropDown(Me.cmbInvoice, "Select ReceivingMasterTable.ReceivingID,ReceivingMasterTable.ReceivingNo From ReceivingMasterTable LEFT OUTER JOIN(SELECT  ISNULL(WIP.RefReceivingID, 0) AS RefReceivingId, SUM(WIPD.TotalQty) AS WIPQty FROM   dbo.WIPProductionDetailTable AS WIPD INNER JOIN  dbo.WIPProductionMasterTable AS WIP ON WIP.DocId = WIPD.DocId WHERE WIPD.DocId <> " & _WIPDocId & " GROUP BY ISNULL(WIP.RefReceivingID, 0)) Wip On Wip.RefReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN (Select ReceivingId, SUM(Qty) as RecQty From ReceivingDetailTable Group By ReceivingId) RecvD on RecvD.ReceivingId = ReceivingMasterTable.ReceivingId WHERE (isnull(RecQty,0)-IsNull(WipQty,0)) > 0 AND VendorID=" & Me.cmbVendor.Value & "")
            ElseIf Condition = "Location" Then
                FillDropDown(Me.cmbLocation, "Select Location_Id, Location_Name From tblDefLocation ORDER BY Sort_Order ASC", False)
            ElseIf Condition = "Item" Then
                'Marked Against Task#201508010 By Ali Ansari to add Sales AccountID in Combo 
                'FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as [Item], ArticleCode as [Code], ArticleGroupName as [Department],ArticleTypeName as [Type/Brand],ArticleCompanyName as [Category], ArticleSizeName as [Size], ArticleColorName as Combination, PurchasePrice as Price, MasterID From ArticleDefView WHERE ArticleDescription <> '' ORDER BY ArticleDescription ASC")
                FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as [Item], ArticleCode as [Code], ArticleGroupName as [Department],ArticleTypeName as [Type/Brand],ArticleCompanyName as [Category], ArticleSizeName as [Size], ArticleColorName as Combination, PurchasePrice as Price, MasterID,SalesAccountId From ArticleDefView WHERE ArticleDescription <> '' ORDER BY ArticleDescription ASC")
                'Marked Against Task#201508010 By Ali Ansari to add Sales AccountID in Combo 
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True
            ElseIf Condition = "UnitPack" Then
                FillDropDown(Me.cmbUnit, "Select ArticlePackId, PackName, PackQty from ArticleDefPackTable WHERE ArticleMasterID=" & Val(Me.cmbItem.ActiveRow.Cells("MasterID").Value.ToString) & "", False)
            ElseIf Condition = "InvoiceList" Then
                FillDropDown(Me.cmbInvoiceList, "Select distinct(docid)_,Invoice_No as InvoiceNo From WIPProductionDetailTable WHERE Invoice_No <> '' ORDER BY Invoice_No ASC")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            Me.grdGatePass.UpdateData()
            Me.grdWIP.UpdateData()
            Me.grdProduction.UpdateData()
            Me.grdStockOut.UpdateData()

            WIPMod = New WIPProductionMasterBE
            WIPMod.DocId = _WIPDocId
            WIPMod.DocNo = Me.txtDocNo.Text
            WIPMod.DocDate = Me.dtpDocDate.Value
            WIPMod.LotNo = Me.txtLotNo.Text
            WIPMod.CustomerCode = Me.cmbVendor.Value
            WIPMod.Remarks = Me.txtRemarks.Text
            WIPMod.UserName = LoginUserName
            WIPMod.EntryDate = Date.Now
            WIPMod.RefReceivingId = 0 'Me.cmbInvoice.SelectedValue
            'WIPMod.Voucher = New VouchersMaster
            'WIPMod.Voucher.VoucherId = 0I
            'WIPMod.Voucher.VoucherNo = Me.txtDocNo.Text
            'WIPMod.Voucher.VoucherDate = Me.dtpDocDate.Value
            'WIPMod.Voucher.VoucherCode = Me.txtDocNo.Text
            'WIPMod.Voucher.VNo = Me.txtDocNo.Text
            'WIPMod.Voucher.FinancialYearId = 1
            'WIPMod.Voucher.LocationId = 1
            'WIPMod.Voucher.Post = True
            'WIPMod.Voucher.Posted_UserName = LoginUserName
            'WIPMod.Voucher.References = "Production"
            'WIPMod.Voucher.Source = Me.Name
            'WIPMod.Voucher.UserName = LoginUserName
            'WIPMod.Voucher.VoucherMaster = WIPMod.Voucher
            'WIPMod.Voucher.VoucherMonth = Me.dtpDocDate.Value.Month
            'WIPMod.Voucher.VoucherTypeId = 1
            'WIPMod.Voucher.ActivityLog = New ActivityLog
            'WIPMod.Voucher.ActivityLog.UserID = LoginUserId
            'WIPMod.Voucher.ActivityLog.User_Name = LoginUserName
            'WIPMod.Voucher.ActivityLog.Source = Me.Name
            'WIPMod.Voucher.ActivityLog.RefNo = Me.txtDocNo.Text
            'WIPMod.Voucher.ActivityLog.LogDateTime = Date.Now
            'WIPMod.Voucher.ActivityLog.FormCaption = "Procduction Process"
            'WIPMod.Voucher.ActivityLog.ApplicationName = "Production"
            'WIPMod.Voucher.VoucherDatail = New List(Of VouchersDetail)
            WIPMod.WIPProductionDetail = New List(Of WIPProductionDetailBE)

            If Me.UltraTabControl1.SelectedTab.Index = 0 Then

                If Me.grdGatePass.RowCount > 0 Then
                    For Each jsRow As Janus.Windows.GridEX.GridEXRow In Me.grdGatePass.GetRows
                        Dim objWipDetail As New WIPProductionDetailBE
                        Dim objmodVD As New VouchersDetail
                        objWipDetail.LocationId = Val(jsRow.Cells("LocationId").Value.ToString)
                        objWipDetail.ArticleDefId = Val(jsRow.Cells("ArticleDefId").Value.ToString)
                        'objWipDetail.AritlceSize = jsRow.Cells("ArticleSize").Value.ToString
                        objWipDetail.PackQty = Val(jsRow.Cells("PackQty").Value.ToString)
                        objWipDetail.Qty = Val(jsRow.Cells("Qty").Value.ToString)
                        objWipDetail.TotalQty = Val(jsRow.Cells("TotalQty").Value.ToString)
                        objWipDetail.Rate = Val(jsRow.Cells("Rate").Value.ToString)
                        objWipDetail.TotalAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        objWipDetail.Comments = jsRow.Cells("Comments").Value.ToString
                        objWipDetail.VehicleNo = jsRow.Cells("VehicleNo").Value.ToString
                        objWipDetail.GatepassNo = jsRow.Cells("GatepassNo").Value.ToString
                        objWipDetail.TruckNo = jsRow.Cells("TruckNo").Value.ToString
                        objWipDetail.TransType = "Receiving"
                        'Altered By Ali Ansari against Task# 201507015 Fill Model In and out qty
                        objWipDetail.InQty = Val(jsRow.Cells("TotalQty").Value.ToString)
                        objWipDetail.InAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        objWipDetail.OutQty = 0
                        objWipDetail.OutAmount = 0
                        'Altered By Ali Ansari against Task# 201507015 saving In and out qty
                        WIPMod.WIPProductionDetail.Add(objWipDetail)
                        'objmodVD.VoucherId = 0I
                        'objmodVD.CoaDetailId = Val(getConfigValueByType("CGSAccountId").ToString) 'IIf(GetGLAccountId(jsRow.Cells("ArticleDefId").Value.ToString, "CGS") > 0, Val(GetGLAccountId(jsRow.Cells("ArticleDefId").Value.ToString, "CGS")), Val(getConfigValueByType("StoreIssuenceAccount").ToString)) 'WIP Debit Account
                        'objmodVD.Comments = "" & jsRow.Cells("ArticleDefId").Text.ToString & "(" & objWipDetail.TotalQty & "X" & objWipDetail.Rate & ")" & ""
                        'objmodVD.contra_coa_detail_id = 0
                        'objmodVD.DebitAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        'objmodVD.CreditAmount = 0
                        'objmodVD.LocationId = 1
                        'WIPMod.Voucher.VoucherDatail.Add(objmodVD)
                        'objmodVD = New VouchersDetail
                        'objmodVD.VoucherId = 0I
                        'objmodVD.CoaDetailId = GetGLAccountId(jsRow.Cells("ArticleDefId").Value.ToString, "Inventory") 'Stock Trade Credit Account
                        'objmodVD.Comments = "" & jsRow.Cells("ArticleDefId").Text.ToString & "(" & objWipDetail.TotalQty & "X" & objWipDetail.Rate & ")" & ""
                        'objmodVD.contra_coa_detail_id = 0
                        'objmodVD.DebitAmount = 0
                        'objmodVD.CreditAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        'objmodVD.LocationId = 1
                        'WIPMod.Voucher.VoucherDatail.Add(objmodVD)

                    Next
                End If

            ElseIf Me.UltraTabControl1.SelectedTab.Index = 1 Then

                If Me.grdWIP.RowCount > 0 Then
                    For Each jsRow As Janus.Windows.GridEX.GridEXRow In Me.grdWIP.GetRows
                        Dim objWipDetail As New WIPProductionDetailBE
                        Dim objmodVD As New VouchersDetail
                        objWipDetail.LocationId = Val(jsRow.Cells("LocationId").Value.ToString)
                        objWipDetail.ArticleDefId = Val(jsRow.Cells("ArticleDefId").Value.ToString)
                        'objWipDetail.AritlceSize = jsRow.Cells("ArticleSize").Value.ToString
                        objWipDetail.PackQty = Val(jsRow.Cells("PackQty").Value.ToString)
                        objWipDetail.Qty = Val(jsRow.Cells("Qty").Value.ToString)
                        objWipDetail.TotalQty = Val(jsRow.Cells("TotalQty").Value.ToString)
                        objWipDetail.Rate = Val(jsRow.Cells("Rate").Value.ToString)
                        objWipDetail.TotalAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        objWipDetail.Comments = jsRow.Cells("Comments").Value.ToString
                        objWipDetail.VehicleNo = jsRow.Cells("VehicleNo").Value.ToString
                        objWipDetail.GatepassNo = jsRow.Cells("GatepassNo").Value.ToString
                        objWipDetail.TruckNo = jsRow.Cells("TruckNo").Value.ToString
                        objWipDetail.TransType = "WIP"
                        'Altered By Ali Ansari against Task# 201507015 Fill Model In and out qty
                        objWipDetail.InQty = 0
                        objWipDetail.InAmount = 0
                        objWipDetail.OutQty = Val(jsRow.Cells("TotalQty").Value.ToString)
                        objWipDetail.OutAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        'Altered By Ali Ansari against Task# 201507015 saving In and out qty
                        WIPMod.WIPProductionDetail.Add(objWipDetail)
                        'objmodVD.VoucherId = 0I
                        'objmodVD.CoaDetailId = Val(getConfigValueByType("CGSAccountId").ToString) 'IIf(GetGLAccountId(jsRow.Cells("ArticleDefId").Value.ToString, "CGS") > 0, Val(GetGLAccountId(jsRow.Cells("ArticleDefId").Value.ToString, "CGS")), Val(getConfigValueByType("StoreIssuenceAccount").ToString)) 'WIP Debit Account
                        'objmodVD.Comments = "" & jsRow.Cells("ArticleDefId").Text.ToString & "(" & objWipDetail.TotalQty & "X" & objWipDetail.Rate & ")" & ""
                        'objmodVD.contra_coa_detail_id = 0
                        'objmodVD.DebitAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        'objmodVD.CreditAmount = 0
                        'objmodVD.LocationId = 1
                        'WIPMod.Voucher.VoucherDatail.Add(objmodVD)
                        'objmodVD = New VouchersDetail
                        'objmodVD.VoucherId = 0I
                        'objmodVD.CoaDetailId = GetGLAccountId(jsRow.Cells("ArticleDefId").Value.ToString, "Inventory") 'Stock Trade Credit Account
                        'objmodVD.Comments = "" & jsRow.Cells("ArticleDefId").Text.ToString & "(" & objWipDetail.TotalQty & "X" & objWipDetail.Rate & ")" & ""
                        'objmodVD.contra_coa_detail_id = 0
                        'objmodVD.DebitAmount = 0
                        'objmodVD.CreditAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        'objmodVD.LocationId = 1
                        'WIPMod.Voucher.VoucherDatail.Add(objmodVD)

                    Next
                End If

            ElseIf Me.UltraTabControl1.SelectedTab.Index = 2 Then

                If Me.grdProduction.RowCount > 0 Then
                    For Each jsRow As Janus.Windows.GridEX.GridEXRow In Me.grdProduction.GetRows
                        Dim objWipDetail As New WIPProductionDetailBE
                        Dim objmodVD As New VouchersDetail

                        objWipDetail.LocationId = Val(jsRow.Cells("LocationId").Value.ToString)
                        objWipDetail.ArticleDefId = Val(jsRow.Cells("ArticleDefId").Value.ToString)
                        'objWipDetail.AritlceSize = jsRow.Cells("ArticleSize").Value.ToString
                        objWipDetail.PackQty = Val(jsRow.Cells("PackQty").Value.ToString)
                        objWipDetail.Qty = Val(jsRow.Cells("Qty").Value.ToString)
                        objWipDetail.TotalQty = Val(jsRow.Cells("TotalQty").Value.ToString)
                        objWipDetail.Rate = Val(jsRow.Cells("Rate").Value.ToString)
                        objWipDetail.TotalAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        objWipDetail.Comments = jsRow.Cells("Comments").Value.ToString
                        objWipDetail.VehicleNo = jsRow.Cells("VehicleNo").Value.ToString
                        objWipDetail.GatepassNo = jsRow.Cells("GatepassNo").Value.ToString
                        objWipDetail.TruckNo = jsRow.Cells("TruckNo").Value.ToString
                        objWipDetail.TransType = "Production"
                        'Altered By Ali Ansari against Task# 201507015 saving In and out qty
                        objWipDetail.InQty = Val(jsRow.Cells("TotalQty").Value.ToString)
                        objWipDetail.InAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        objWipDetail.OutQty = 0
                        objWipDetail.OutAmount = 0
                        'Altered By Ali Ansari against Task# 201507015 saving In and out qty
                        WIPMod.WIPProductionDetail.Add(objWipDetail)
                        'objmodVD.VoucherId = 0I
                        'objmodVD.CoaDetailId = GetGLAccountId(jsRow.Cells("ArticleDefId").Value.ToString, "Inventory") 'WIP Debit Account
                        'objmodVD.Comments = "" & jsRow.Cells("ArticleDefId").Text.ToString & "(" & objWipDetail.TotalQty & "X" & objWipDetail.Rate & ")" & ""
                        'objmodVD.contra_coa_detail_id = 0
                        'objmodVD.DebitAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        'objmodVD.CreditAmount = 0
                        'objmodVD.LocationId = 1
                        'WIPMod.Voucher.VoucherDatail.Add(objmodVD)

                        'objmodVD = New VouchersDetail
                        'objmodVD.VoucherId = 0I
                        'objmodVD.CoaDetailId = Val(getConfigValueByType("CGSAccountId").ToString) 'IIf(GetGLAccountId(jsRow.Cells("ArticleDefId").Value.ToString, "CGS") > 0, Val(GetGLAccountId(jsRow.Cells("ArticleDefId").Value.ToString, "CGS")), Val(getConfigValueByType("StoreIssuenceAccount").ToString)) 'Stock Trade Credit Account
                        'objmodVD.Comments = "" & jsRow.Cells("ArticleDefId").Text.ToString & "(" & objWipDetail.TotalQty & "X" & objWipDetail.Rate & ")" & ""
                        'objmodVD.contra_coa_detail_id = 0
                        'objmodVD.DebitAmount = 0
                        'objmodVD.CreditAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        'objmodVD.LocationId = 1
                        'WIPMod.Voucher.VoucherDatail.Add(objmodVD)
                    Next
                End If

            ElseIf Me.UltraTabControl1.SelectedTab.Index = 3 Then
                If Me.grdStockOut.RowCount > 0 Then
                    For Each jsRow As Janus.Windows.GridEX.GridEXRow In Me.grdStockOut.GetRows
                        Dim objWipDetail As New WIPProductionDetailBE
                        Dim objmodVD As New VouchersDetail
                        objWipDetail.LocationId = Val(jsRow.Cells("LocationId").Value.ToString)
                        objWipDetail.ArticleDefId = Val(jsRow.Cells("ArticleDefId").Value.ToString)
                        'objWipDetail.AritlceSize = jsRow.Cells("ArticleSize").Value.ToString
                        objWipDetail.PackQty = Val(jsRow.Cells("PackQty").Value.ToString)
                        objWipDetail.Qty = Val(jsRow.Cells("Qty").Value.ToString)
                        objWipDetail.TotalQty = Val(jsRow.Cells("TotalQty").Value.ToString)
                        objWipDetail.Rate = Val(jsRow.Cells("Rate").Value.ToString)
                        objWipDetail.TotalAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        objWipDetail.Comments = jsRow.Cells("Comments").Value.ToString
                        objWipDetail.VehicleNo = jsRow.Cells("VehicleNo").Value.ToString
                        objWipDetail.GatepassNo = jsRow.Cells("GatepassNo").Value.ToString
                        objWipDetail.TruckNo = jsRow.Cells("TruckNo").Value.ToString
                        objWipDetail.TransType = "StockOut"
                        'Altered By Ali Ansari against Task# 201507015 saving In and out qty
                        objWipDetail.OutQty = Val(jsRow.Cells("TotalQty").Value.ToString)
                        objWipDetail.OutAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        objWipDetail.InQty = 0
                        objWipDetail.InAmount = 0
                        'Altered By Ali Ansari against Task# 201507015 saving In and out qty
                        WIPMod.WIPProductionDetail.Add(objWipDetail)



                        'objmodVD.VoucherId = 0I
                        'objmodVD.CoaDetailId = Me.cmbVendor.Value  'WIP Debit Account
                        'objmodVD.Comments = "" & jsRow.Cells("ArticleDefId").Text.ToString & "(" & objWipDetail.TotalQty & "X" & objWipDetail.Rate & ")" & ""
                        'objmodVD.contra_coa_detail_id = 0
                        'objmodVD.DebitAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        'objmodVD.CreditAmount = 0
                        'objmodVD.LocationId = 1
                        'WIPMod.Voucher.VoucherDatail.Add(objmodVD)

                        'objmodVD = New VouchersDetail
                        'objmodVD.VoucherId = 0I
                        'objmodVD.CoaDetailId = GetGLAccountId(jsRow.Cells("ArticleDefId").Value.ToString, "Inventory") 'Val(getConfigValueByType("StoreCreditAccount").ToString) 'Stock Trade Credit Account
                        'objmodVD.Comments = "" & jsRow.Cells("ArticleDefId").Text.ToString & "(" & objWipDetail.TotalQty & "X" & objWipDetail.Rate & ")" & ""
                        'objmodVD.contra_coa_detail_id = 0
                        'objmodVD.DebitAmount = 0
                        'objmodVD.CreditAmount = Val(jsRow.Cells("TotalAmount").Value.ToString)
                        'objmodVD.LocationId = 1
                        'WIPMod.Voucher.VoucherDatail.Add(objmodVD)



                    Next
                End If
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            If Condition = "Master" Then
                dt = New WIPProductionDAL().GetAll
                dt.AcceptChanges()
                Me.grdProductionHistory.DataSource = dt
                Me.grdProductionHistory.RetrieveStructure()
                Me.grdProductionHistory.RootTable.Columns(0).Visible = False
                Me.grdProductionHistory.RootTable.Columns("CustomerCode").Visible = False
                Me.grdProductionHistory.RootTable.Columns("DocDate").TotalFormatString = "dd/MMM/yyyy"
                Me.grdProductionHistory.AutoSizeColumns()
            ElseIf Condition = "Detail" Then

                Dim dtItem As New DataTable
                'dtItem = GetDataTable("Select ArticleId, ArticleDescription as Item,ArticleCode as [Item Code], IsNull(PurchasePrice,0) as Rate, IsNull(PackQty,0) as [Pack Qty] From ArticleDefView WHERE ArticleDescription <> '' Order By ArticleDescription ASC")
                dtItem = GetDataTable("Select ArticleId, ArticleDescription as Item,ArticleCode as [Item Code], IsNull(PurchasePrice,0) as Rate, IsNull(PackQty,0) as [Pack Qty],isnull(SalesAccountId,0) as  SalesAccountId From ArticleDefView WHERE ArticleDescription <> '' Order By ArticleDescription ASC")
                dtItem.AcceptChanges()

                Dim dtLocation As New DataTable
                dtLocation = GetDataTable("Select Location_Id, Location_Name From tblDefLocation Order By Sort_Order ASC")
                dtLocation.AcceptChanges()

                dt = New WIPProductionDAL().GetDetailRecords(_WIPDocId, "Receiving")
                dt.AcceptChanges()
                dt.Columns("TotalAmount").Expression = "(IsNull([TotalQty],0)*IsNull([Rate],0))"
                dt.AcceptChanges()
                Me.grdGatePass.DataSource = dt
                grdGatePass.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "Location_Id", "Location_Name")
                grdGatePass.RootTable.Columns("ArticleDefId").ValueList.PopulateValueList(dtItem.DefaultView, "ArticleId", "Item")

                dt = New WIPProductionDAL().GetDetailRecords(_WIPDocId, "WIP")
                dt.AcceptChanges()
                dt.Columns("TotalAmount").Expression = "(IsNull([TotalQty],0)*IsNull([Rate],0))"
                dt.AcceptChanges()
                Me.grdWIP.DataSource = dt
                grdWIP.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "Location_Id", "Location_Name")
                grdWIP.RootTable.Columns("ArticleDefId").ValueList.PopulateValueList(dtItem.DefaultView, "ArticleId", "Item")

                dt = New WIPProductionDAL().GetDetailRecords(_WIPDocId, "Production")
                dt.AcceptChanges()
                dt.Columns("TotalAmount").Expression = "(IsNull([TotalQty],0)*IsNull([Rate],0))"
                dt.AcceptChanges()
                Me.grdProduction.DataSource = dt
                grdProduction.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "Location_Id", "Location_Name")
                grdProduction.RootTable.Columns("ArticleDefId").ValueList.PopulateValueList(dtItem.DefaultView, "ArticleId", "Item")



                ApplyGridSettings()




            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtLotNo.TextLength <= 0 Then
                ShowErrorMessage("Please enter lot no.")
                Me.txtLotNo.Focus()
                Return False
            End If
            Dim dt As New DataTable
            dt = GetDataTable("Select LotNo From WIPProductionMasterTable WHERE DocId <> " & _WIPDocId & "")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0).ToString.ToUpper = Me.txtLotNo.Text.ToUpper Then
                    ShowErrorMessage("Lot No# [" & Me.txtLotNo.Text.Replace("'", "''") & "] Is Already Exist.")
                    Me.txtLotNo.Focus()
                    Return False
                End If
            End If
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Invalid Customer")
                Me.cmbVendor.Focus()
                Return False
            End If
            If Me.cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select Customer.")
                Me.cmbVendor.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            _WIPDocId = 0I
            Me.btnSave.Text = "&Save"
            Me.txtDocNo.Text = GetDocumentNo()
            Me.dtpDocDate.Value = Date.Now
            Me.txtLotNo.Text = String.Empty
            Me.cmbVendor.Rows(0).Activate()
            FillCombos("PO")
            FillCombos("InvoiceList")
            Me.txtRemarks.Text = String.Empty
            Me.UltraTabControl1.Tabs(0).Enabled = True
            Me.UltraTabControl1.Tabs(1).Enabled = True
            Me.UltraTabControl1.Tabs(2).Enabled = True
            Me.UltraTabControl1.Tabs(3).Enabled = True
            Me.txtInvoiceNo.Text = GetInvoiceNo("SI-" & Me.dtpInvoiceDate.Value.ToString("yyMM") & "-")
            Me.dtpInvoiceDate.Value = Date.Now
            If Not Me.cmbInvoiceList.SelectedIndex = -1 Then Me.cmbInvoiceList.SelectedIndex = 0
            GetAllRecords("Master")
            GetAllRecords("Detail")
            Me.ToolStrip1.Enabled = True
            GetInvoiceDetail()
            DetailClear()
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)
            Me.txtLotNo.Focus()
            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            If New WIPProductionDAL().Add(WIPMod) Then
                Return True
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
        Try
            If New WIPProductionDAL().Update(WIPMod) Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmProductionProcessing_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombos()
            FillCombos("Location")
            FillCombos("Item")
            FillCombos("UnitPack")
            ReSetControls()
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords(Optional ByVal Condition As String = "")
        Try
            If Me.grdProductionHistory.RowCount = 0 Then Exit Sub
            'FillCombos()

            'RemoveHandler cmbInvoice.SelectedIndexChanged, AddressOf Me.cmbInvoice_SelectedIndexChanged
            'RemoveHandler cmbVendor.Leave, AddressOf Me.cmbVendor_Leave

            _WIPDocId = Val(Me.grdProductionHistory.GetRow.Cells("DocId").Value.ToString)
            'Dim dtData As DataTable = CType(Me.cmbInvoice.DataSource, DataTable)
            'dtData.AcceptChanges()
            'If dtData IsNot Nothing Then
            '    If dtData.Rows.Count > 0 Then
            '        Dim dr() As DataRow = dtData.Select("ReceivingId=" & Val(Me.grdProductionHistory.GetRow.Cells("RefReceivingId").Value.ToString) & "")
            '        If Not dr.Length > 0 Then
            '            Dim id As Integer = Me.cmbInvoice.SelectedIndex
            '            FillCombos("PO")
            '            Me.cmbInvoice.SelectedIndex = id
            '        End If
            '    End If
            'End If

            'Me.cmbInvoice.SelectedValue = Val(Me.grdProductionHistory.GetRow.Cells("RefReceivingId").Value.ToString)
            Me.txtDocNo.Text = Me.grdProductionHistory.GetRow.Cells("DocNo").Value.ToString
            Me.dtpDocDate.Value = Me.grdProductionHistory.GetRow.Cells("DocDate").Value
            Me.cmbVendor.Value = Val(Me.grdProductionHistory.GetRow.Cells("CustomerCode").Value.ToString)
            Me.txtLotNo.Text = Me.grdProductionHistory.GetRow.Cells("LotNo").Value.ToString
            Me.txtRemarks.Text = Me.grdProductionHistory.GetRow.Cells("Remarks").Value.ToString
            GetAllRecords("Detail")
            GetInvoiceDetail()
            Me.txtLotNo.Focus()
            Me.btnSave.Text = "&Update"

            'AddHandler cmbInvoice.SelectedIndexChanged, AddressOf Me.cmbInvoice_SelectedIndexChanged
            'AddHandler cmbVendor.Leave, AddressOf Me.cmbVendor_Leave

            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdProductionHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdProductionHistory.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdProductionHistory.RowCount = 0 Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("WIP" & "-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "WIPProductionMasterTable", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("WIP" & "-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "WIPProductionMasterTable", "DocNo")
            Else
                Return GetNextDocNo("WIP", 6, "WIPProductionMasterTable", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDocID() As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_Production " & Val(Me.grdProductionHistory.GetRow.Cells("DocId").Value.ToString) & ""
            Dim dtData As New DataTable
            dtData = GetDataTable(strSQL)

            Return dtData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub PrintProductionProcessingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintProductionProcessingToolStripMenuItem.Click
        Try
            If Me.grdProductionHistory.RowCount = 0 Then Exit Sub
            'AddRptParam("@EmpId", Me.grd.GetRow.Cells("EmployeeId").Value.ToString)
            ShowReport("rptProductionProcessing", , , , , , , GetDocID)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdWIP_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdWIP.Enter
        Try
            Me.grdWIP.MoveNext()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdWIP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdWIP.KeyDown
        Try
            If Not Me.grdWIP.CurrentColumn.Key = "ArticleDefId" Then Exit Sub
            If e.KeyCode = Keys.F1 Then
                ApplyStyleSheet(frmGetItemDetail)
                If frmGetItemDetail.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Me.grdWIP.GetRow.Cells("ArticleDefId").Value = frmGetItemDetail.cmbItem.Value
                    Me.grdWIP.GetRow.Cells("Qty").Value = Val(frmGetItemDetail.txtQty.Text)
                    Me.grdWIP.GetRow.Cells("PackQty").Value = Val(frmGetItemDetail.txtPackQty.Text)
                    Me.grdWIP.GetRow.Cells("TotalQty").Value = Val(frmGetItemDetail.txtTotalQty.Text)
                    Me.grdWIP.GetRow.Cells("Rate").Value = Val(frmGetItemDetail.txtRate.Text)
                    Me.grdWIP.GetRow.Cells("TotalAmount").Value = Val(frmGetItemDetail.txtTotalAmount.Text)
                    grdWIP_Enter(Nothing, Nothing)
                    Me.grdWIP.Row = -1
                    Me.grdWIP.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseFocusCellFormatStyle
                    Me.grdWIP.Focus()
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdProduction_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdProduction.ColumnButtonClick
        Try
            If e.Column.Key = "btnDelete2" Then Me.grdProduction.GetRow.Delete()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdProduction_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdProduction.Enter
        Try
            Me.grdProduction.MoveNext()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdProduction_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdProduction.KeyDown
        Try

            If Not Me.grdProduction.CurrentColumn.Key = "ArticleDefId" Then Exit Sub
            If e.KeyCode = Keys.F1 Then
                ApplyStyleSheet(frmGetItemDetail)
                If frmGetItemDetail.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Me.grdProduction.GetRow.Cells("ArticleDefId").Value = frmGetItemDetail.cmbItem.Value
                    Me.grdProduction.GetRow.Cells("Qty").Value = Val(frmGetItemDetail.txtQty.Text)
                    Me.grdProduction.GetRow.Cells("PackQty").Value = Val(frmGetItemDetail.txtPackQty.Text)
                    Me.grdProduction.GetRow.Cells("TotalQty").Value = Val(frmGetItemDetail.txtTotalQty.Text)
                    Me.grdProduction.GetRow.Cells("Rate").Value = Val(frmGetItemDetail.txtRate.Text)
                    Me.grdProduction.GetRow.Cells("TotalAmount").Value = Val(frmGetItemDetail.txtTotalAmount.Text)
                    grdProduction_Enter(Nothing, Nothing)
                    Me.grdProduction.Row = -1
                    Me.grdProduction.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseFocusCellFormatStyle
                    Me.grdProduction.Focus()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdStockOut_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdStockOut.ColumnButtonClick
        Try
            If e.Column.Key = "btnDelete3" Then Me.grdStockOut.GetRow.Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdStockOut_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdStockOut.Enter
        Try
            Me.grdStockOut.MoveNext()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdStockOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdStockOut.KeyDown
        Try
            If Not Me.grdStockOut.CurrentColumn.Key = "ArticleDefId" Then Exit Sub
            If e.KeyCode = Keys.F1 Then
                ApplyStyleSheet(frmGetItemDetail)
                If frmGetItemDetail.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Me.grdStockOut.GetRow.Cells("ArticleDefId").Value = frmGetItemDetail.cmbItem.Value
                    Me.grdStockOut.GetRow.Cells("Qty").Value = Val(frmGetItemDetail.txtQty.Text)
                    Me.grdStockOut.GetRow.Cells("PackQty").Value = Val(frmGetItemDetail.txtPackQty.Text)
                    Me.grdStockOut.GetRow.Cells("TotalQty").Value = Val(frmGetItemDetail.txtTotalQty.Text)
                    Me.grdStockOut.GetRow.Cells("Rate").Value = Val(frmGetItemDetail.txtRate.Text)
                    Me.grdStockOut.GetRow.Cells("TotalAmount").Value = Val(frmGetItemDetail.txtTotalAmount.Text)
                    grdStockOut_Enter(Nothing, Nothing)
                    Me.grdStockOut.Row = -1
                    Me.grdStockOut.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseFocusCellFormatStyle
                    Me.grdStockOut.Focus()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Function GetWIPNo() As String
    '    Try
    '        'If Me.txtPONo.Text = "" Then
    '        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
    '            Return GetSerialNo("WIP" & "-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "WIPProductionMasterTable", "DocNo")
    '        ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
    '            Return GetNextDocNo("WIP" & "-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "WIPProductionMasterTable", "DocNo")
    '        Else
    '            Return GetNextDocNo("WIP", 6, "WIPProductionMasterTable", "DocNo")
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function


    Private Sub grdWIP_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdWIP.ColumnButtonClick
        Try
            If e.Column.Key = "btnDelete1" Then Me.grdWIP.GetRow.Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub cmbInvoice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If IsOpenForm = True Then
    '            Dim dt As DataTable = New WIPProductionDAL().GetRecvDetailRecords(Me.cmbInvoice.SelectedValue)
    '            dt.AcceptChanges()
    '            Me.grdWIP.DataSource = dt
    '        Else
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbVendor_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.Leave
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            FillCombos("PO")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            FillCombos("UnitPack")
            If Me.cmbUnit.SelectedIndex = -1 Then
                Dim dt As New DataTable
                dt = CType(Me.cmbUnit.DataSource, DataTable)
                If dt IsNot Nothing Then
                    dt.AcceptChanges()
                    Dim dr As DataRow
                    dr = dt.NewRow
                    dr(0) = 0
                    dr(1) = "Loose"
                    dr(2) = 1
                    dt.Rows.Add(dr)
                    dr = dt.NewRow
                    dr(0) = 0
                    dr(1) = "Pack"
                    dr(2) = 1
                    dt.Rows.Add(dr)
                    dt.AcceptChanges()
                End If
            End If
            Me.txtPrice.Text = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbUnit.SelectedIndex = -1 Then Exit Sub

            If Me.cmbUnit.Text <> "Loose" Then
                Me.txtPackQty.Text = CType(Me.cmbUnit.SelectedItem, DataRowView).Row("PackQty").ToString
            Else
                Me.txtPackQty.Text = 1
            End If
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetTotal()
        Try
            Dim dblTotalQty As Double = IIf(Val(Me.txtPackQty.Text) > 0, (Val(Me.txtQty.Text) * Val(Me.txtPackQty.Text)), Val(Me.txtQty.Text))
            Dim dblTotalAmount As Double = dblTotalQty * Val(Me.txtPrice.Text)
            Me.txtTotalQty.Text = dblTotalQty
            Me.txtTotalAmount.Text = dblTotalAmount
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged, txtPrice.TextChanged, txtPackQty.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If IsValide_Add() = False Then Exit Sub

            Dim dt As New DataTable
            If Me.UltraTabControl1.SelectedTab.Index = 0 Then
                dt = CType(Me.grdGatePass.DataSource, DataTable)
            ElseIf Me.UltraTabControl1.SelectedTab.Index = 1 Then
                dt = CType(Me.grdWIP.DataSource, DataTable)
            ElseIf Me.UltraTabControl1.SelectedTab.Index = 2 Then
                dt = CType(Me.grdProduction.DataSource, DataTable)
            ElseIf Me.UltraTabControl1.SelectedTab.Index = 3 Then
                dt = CType(Me.grdStockOut.DataSource, DataTable)
            End If



            Dim dr As DataRow = dt.NewRow
            dr(enmGrdDetail.LocationId) = Me.cmbLocation.SelectedValue
            dr(enmGrdDetail.ArticleId) = Me.cmbItem.Value
            dr(enmGrdDetail.PackQty) = Val(Me.txtPackQty.Text)
            dr(enmGrdDetail.Qty) = Val(Me.txtQty.Text)
            dr(enmGrdDetail.TotalQty) = Val(Me.txtTotalQty.Text)
            dr(enmGrdDetail.Rate) = Val(Me.txtPrice.Text)
            dr(enmGrdDetail.TotalAmount) = Val(Me.txtTotalAmount.Text)
            dr(enmGrdDetail.Comments) = String.Empty
            '
            dr(enmGrdDetail.SalesAccountId) = Val(Me.cmbItem.ActiveRow.Cells("SalesAccountId").Value.ToString)


            dt.Rows.Add(dr)
            dt.AcceptChanges()




            If Me.UltraTabControl1.SelectedTab.Index = 0 Then
                Me.UltraTabControl1.Tabs(0).Enabled = True
                Me.UltraTabControl1.Tabs(1).Enabled = False
                Me.UltraTabControl1.Tabs(2).Enabled = False
                Me.UltraTabControl1.Tabs(3).Enabled = False
            ElseIf Me.UltraTabControl1.SelectedTab.Index = 1 Then
                Me.UltraTabControl1.Tabs(0).Enabled = False
                Me.UltraTabControl1.Tabs(1).Enabled = True
                Me.UltraTabControl1.Tabs(2).Enabled = False
                Me.UltraTabControl1.Tabs(3).Enabled = False
            ElseIf Me.UltraTabControl1.SelectedTab.Index = 2 Then
                Me.UltraTabControl1.Tabs(0).Enabled = False
                Me.UltraTabControl1.Tabs(1).Enabled = False
                Me.UltraTabControl1.Tabs(2).Enabled = True
                Me.UltraTabControl1.Tabs(3).Enabled = False
            ElseIf Me.UltraTabControl1.SelectedTab.Index = 3 Then
                Me.UltraTabControl1.Tabs(0).Enabled = False
                Me.UltraTabControl1.Tabs(1).Enabled = False
                Me.UltraTabControl1.Tabs(2).Enabled = False
                Me.UltraTabControl1.Tabs(3).Enabled = True
            End If

            DetailClear()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function IsValide_Add() As Boolean
        Try
            If Me.cmbItem.IsItemInList = False Then Return False
            If Me.cmbItem.ActiveRow Is Nothing Then Return False

            If Me.cmbItem.Value <= 0 Then
                ShowErrorMessage("Please select item.")
                Me.cmbItem.Focus()
                Return False
            End If
            If Val(Me.txtQty.Text) <= 0 Then
                ShowErrorMessage("Qty should be greater than zero.")
                Me.txtQty.Focus()
                Return False
            End If
            If Val(Me.txtPrice.Text) = 0 Then
                ShowErrorMessage("Please enter the price.")
                Me.txtQty.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub DetailClear()
        Try
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            Me.txtQty.Text = String.Empty
            Me.txtTotalQty.Text = String.Empty
            Me.txtPrice.Text = String.Empty
            Me.txtTotalAmount.Text = String.Empty
            Me.cmbItem.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try


            Dim id As Integer = 0I


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnInvoiceSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvoiceSave.Click
        Me.Cursor = Cursors.WaitCursor
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand

        Try


            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 120

            Dim lngVoucherMasterId As Integer = 0I


            Dim str As String = String.Empty
            Dim strInvoiceNo As String = String.Empty


            If Me.btnInvoiceSave.Text = "Invoice Save" Then
                strInvoiceNo = GetInvoiceNo("SI-" & Me.dtpInvoiceDate.Value.ToString("yyMM") & "-", trans)
                str = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                       & " cheque_no, cheque_date,post,Source,voucher_code)" _
                                       & " VALUES(" & enmGrdDetail.LocationId & ", 1,  7 , N'" & Me.txtInvoiceNo.Text & "', N'" & Me.dtpInvoiceDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                       & " NULL, NULL, 1 ,N'" & Me.Name & "',N'" & Me.txtInvoiceNo.Text & "')" _
                                       & " SELECT @@IDENTITY"
                cmd.CommandText = str
                lngVoucherMasterId = cmd.ExecuteScalar()

            Else
                lngVoucherMasterId = GetVoucherId2(Me.Name, Me.txtInvoiceNo.Text)
                str = "update  tblVoucher set location_id = " & enmGrdDetail.LocationId & ",  finiancial_year_id = 1, voucher_type_id = 7 , voucher_no =  N'" & Me.txtInvoiceNo.Text & "', voucher_date  =  N'" & Me.dtpInvoiceDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "' ,voucher_code = N'" & Me.txtInvoiceNo.Text & "' where voucher_id = " & lngVoucherMasterId & " "
                cmd.CommandText = str


                cmd.CommandText = ""
                cmd.CommandText = "Delete From WIPProductionDetailTable WHERE DocId=" & _WIPDocId & " AND Invoice_No='" & Me.txtInvoiceNo.Text & "'"
                cmd.ExecuteNonQuery()
                strInvoiceNo = Me.txtInvoiceNo.Text

            End If

            


            str = String.Empty
            cmd.CommandText = ""
            cmd.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
            cmd.ExecuteNonQuery()
            ''''Voucher Heder Posting
            ''''''''''''''''''''''''

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdStockOut.GetRows
                cmd.CommandText = ""
                Dim strSQL As String = "INSERT INTO WIPProductionDetailTable(DocId, LocationId, ArticleDefId,PackQty,Qty,TotalQty,Rate,TotalAmount,TransType,Comments,VehicleNo,GatePassNo,TruckNo,Invoice_No,Invoice_Date) " _
                     & " Values(" & _WIPDocId & ", " & Val(r.Cells(enmGrdDetail.LocationId).Value.ToString) & "," & Val(r.Cells(enmGrdDetail.ArticleId).Value.ToString) & "," & Val(r.Cells(enmGrdDetail.PackQty).Value.ToString) & "," & Val(r.Cells(enmGrdDetail.Qty).Value.ToString) & "," & Val(r.Cells(enmGrdDetail.TotalQty).Value.ToString) & "," & Val(r.Cells(enmGrdDetail.Rate).Value.ToString) & "," & Val(r.Cells(enmGrdDetail.TotalAmount).Value.ToString) & ",'StockOut','" & r.Cells("Comments").Value.ToString.Replace("'", "''") & "', '" & r.Cells("VehicleNo").Value.ToString.Replace("'", "''") & "','" & r.Cells("GatePassNo").Value.ToString.Replace("'", "''") & "','" & r.Cells("TruckNo").Value.ToString.Replace("'", "''") & "','" & strInvoiceNo & "',Convert(DateTime,'" & Me.dtpInvoiceDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102))"

                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()
                ''''Voucher Detail Posting


                ''''Voucher Detail Posting
                cmd.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
                               & " VALUES(" & lngVoucherMasterId & ", " & Val(r.Cells(enmGrdDetail.LocationId).Value.ToString) & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & (Val(r.Cells(enmGrdDetail.Qty).Value.ToString) * Val(r.Cells(enmGrdDetail.PackQty).Value.ToString)) * Val(r.Cells(enmGrdDetail.Rate).Value.ToString) & ",0)"
                cmd.ExecuteNonQuery()

                cmd.CommandText = ""



                ' str = Val(r.Cells(enmGrdDetail.SalesAccountId).Value.ToString)
                cmd.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
                               & " VALUES(" & lngVoucherMasterId & ", " & Val(r.Cells(enmGrdDetail.LocationId).Value.ToString) & ", " & Val(r.Cells("SalesAccountId").Value.ToString) & ", 0," & (Val(r.Cells(enmGrdDetail.Qty).Value.ToString) * Val(r.Cells(enmGrdDetail.PackQty).Value.ToString)) * Val(r.Cells(enmGrdDetail.Rate).Value.ToString) & ")"
                cmd.ExecuteNonQuery()





            Next
            trans.Commit()

            FillCombos("InvoiceList")
            btnClear_Click(Nothing, Nothing)

        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub GetInvoiceDetail()
        Try

            Dim dtItem As New DataTable
            dtItem = GetDataTable("Select ArticleId, ArticleDescription as Item,ArticleCode as [Item Code], IsNull(PurchasePrice,0) as Rate, IsNull(PackQty,0) as [Pack Qty],isnull(SalesAccountId,0) as SalesAccountId From ArticleDefView WHERE ArticleDescription <> '' Order By ArticleDescription ASC")
            dtItem.AcceptChanges()


            Dim dtLocation As New DataTable
            dtLocation = GetDataTable("Select Location_Id, Location_Name From tblDefLocation Order By Sort_Order ASC")
            dtLocation.AcceptChanges()

            Dim dt As DataTable = New WIPProductionDAL().GetInvoiceDetailRecords(_WIPDocId, Me.txtInvoiceNo.Text)
            dt.AcceptChanges()
            dt.Columns("TotalAmount").Expression = "(IsNull([TotalQty],0)*IsNull([Rate],0))"
            dt.AcceptChanges()
            Me.grdStockOut.DataSource = dt
            grdStockOut.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "Location_Id", "Location_Name")
            grdStockOut.RootTable.Columns("ArticleDefId").ValueList.PopulateValueList(dtItem.DefaultView, "ArticleId", "Item")
            grdStockOut.RootTable.Columns("ArticleDefId").ValueList.PopulateValueList(dtItem.DefaultView, "ArticleId", "Item")


            ''''





        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function GetInvoiceNo(ByVal Prefix As String, Optional ByVal trans As OleDb.OleDbTransaction = Nothing) As String
        Try
            Dim dt As New DataTable
            Dim serial As Integer = 0I
            Dim serialno As String = String.Empty
            dt = GetDataTable("Select IsNull(Max(Right(Invoice_No,5)),0)+1 As SerialNo From WIPProductionDetailTable WHERE Left(Invoice_No," & Prefix.Length & ")='" & Prefix & "' ", trans)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                serial = Val(dt.Rows(0).Item(0).ToString)
            Else
                serial = 1
            End If
            serialno = Prefix & Microsoft.VisualBasic.Right("0000" & CStr(serial), 5)
            Return serialno
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Me.btnInvoiceSave.Text = "Invoice Save"
            Me.txtInvoiceNo.Text = GetInvoiceNo("SI-" & Me.dtpInvoiceDate.Value.ToString("yyMM") & "-")
            Me.dtpInvoiceDate.Value = Date.Now
            ''''''''''''''''
            Me.txtInvoiceNo.Text = String.Empty
            Me.cmbInvoiceList.SelectedIndex = 0
            Me.btnInvoiceDelete.Enabled = False

            '''''''''''''''
            GetInvoiceDetail()
            Me.ToolStrip1.Enabled = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 3 Then
                Me.ToolStrip1.Enabled = False
            Else
                Me.ToolStrip1.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbInvoiceList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbInvoiceList.SelectedIndexChanged
        Try
            Dim dt As New DataTable
            Dim strquery As String = String.Empty
            If cmbInvoiceList.SelectedIndex > 0 Then
                'strquery = "Select distinct(invoice_no),Invoice_date,docid  From WIPProductionDetailTable WHERE Invoice_No =  '" & cmbInvoiceList.SelectedValue & "'"
                'dt = GetDataTable(strquery)
                'dt.AcceptChanges()
                'If dt.Rows.Count > 0 Then
                '    txtInvoiceNo.Text = dt.Rows(0).Item(0).ToString

                '    dtpInvoiceDate.Value = dt.Rows(0).Item(1).ToString
                '    TxtDocId.Text = dt.Rows(0).Item(2).ToString

                '    Dim WIPDetail As New WIPProductionDetailBE

                '    WIPDetail.DocId = TxtDocId.Text
                _WIPDocId = cmbInvoiceList.SelectedValue
                Me.txtInvoiceNo.Text = cmbInvoiceList.Text
                Me.btnInvoiceSave.Text = "Invoice Update"
                GetInvoiceDetail()
                Me.btnInvoiceDelete.Enabled = True
            Else
                _WIPDocId = 0
                Me.txtInvoiceNo.Text = ""
                Me.btnInvoiceSave.Text = "Invoice Save"
                Me.btnInvoiceDelete.Enabled = False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnInvoiceDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInvoiceDelete.Click
        Me.Cursor = Cursors.WaitCursor
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand

        Try


            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 120
            Dim strInvoiceNo As String = String.Empty
            If Me.btnInvoiceSave.Text = "Invoice Update" Then
                cmd.CommandText = ""
                cmd.CommandText = "Delete From WIPProductionDetailTable WHERE DocId=" & _WIPDocId & " AND Invoice_No='" & Me.txtInvoiceNo.Text & "'"
                cmd.ExecuteNonQuery()
            End If

            trans.Commit()

            FillCombos("InvoiceList")
            btnClear_Click(Nothing, Nothing)


        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.grdProductionHistory.RowCount = 0 Then Exit Sub

            AddRptParam("@docid", Me.cmbInvoiceList.SelectedValue)
            AddRptParam("@InvoiceNo", Me.cmbInvoiceList.Text)
            ShowReport("rptproductioninvoice")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdProductionHistory.GetRow Is Nothing AndAlso grdProductionHistory.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmProductionProcessing"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdProductionHistory.CurrentRow.Cells(1).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Production processing (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Inventory
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class