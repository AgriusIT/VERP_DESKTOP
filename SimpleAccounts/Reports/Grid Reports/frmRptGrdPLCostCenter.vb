Imports SBUtility.Utility
Imports SBDal
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Net
Imports SBModel
Public Class frmRptGrdPLCostCenter
    Enum enmGrdPLCostCenter
        CostCenterId
        CostCenter
        CostCenterGroup
        PL1
        PL2
        PL3
        PL4
        PL5
        PL6
        PL7
        PL8
        SumPL
    End Enum
    Public Sub ApplyGridSetting()
        Try
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.CostCenterId).Visible = False

            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL1).Caption = "Net Sales"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL2).Caption = "CG Sold"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL3).Caption = "NOP InCome"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL4).Caption = "Admin Exp"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL5).Caption = "Selling Exp"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL6).Caption = "Other OP Exp"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL7).Caption = "Finance Cost"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL8).Caption = "Taxation"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.SumPL).Caption = "Total"

            Me.GrdPLCostCenter.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL4).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL5).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL6).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL7).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL8).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.SumPL).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL1).FormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL2).FormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL3).FormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL4).FormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL5).FormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL6).FormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL7).FormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL8).FormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.SumPL).FormatString = "N"

            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL5).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL6).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL7).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL8).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.SumPL).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL5).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL6).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL7).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL8).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.SumPL).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL1).TotalFormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL2).TotalFormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL3).TotalFormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL4).TotalFormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL5).TotalFormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL6).TotalFormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL7).TotalFormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.PL8).TotalFormatString = "N"
            Me.GrdPLCostCenter.RootTable.Columns(enmGrdPLCostCenter.SumPL).TotalFormatString = "N"
            Me.GrdPLCostCenter.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnGenerate.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            Me.BtnGenerate.Enabled = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Generate" Then
                    Me.BtnGenerate.Enabled = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Choose Fielder" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Function GetRecords() As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()

        Dim str As String = String.Empty
        Dim Dt As New DataTable
        Dim Da As SqlClient.SqlDataAdapter
        Try
            'str = "SELECT dbo.tblDefCostCenter.CostCenterID, dbo.tblDefCostCenter.Name, dbo.tblDefCostCenter.CostCenterGroup, ISNULL(PL1.PL1, 0) AS PL1, ISNULL(PL2.PL2,0) AS PL2, ISNULL(PL3.PL3, 0) AS PL3,  ISNULL(PL4.PL4, 0) AS PL4, ISNULL(PL5.PL5, 0) AS PL5, ISNULL(PL6.PL6, 0) AS PL6, ISNULL(PL7.PL7, 0)AS PL7,ISNULL(PL8.PL8, 0) AS PL8, (ISNULL(PL1.PL1, 0)+ISNULL(PL2.PL2,0)+ISNULL(PL3.PL3, 0)+ISNULL(PL4.PL4, 0)+ISNULL(PL5.PL5, 0)+ISNULL(PL6.PL6, 0)+ISNULL(PL7.PL7,0)+ISNULL(PL8.PL8,0)) as SumPL " _
            ' & " FROM  dbo.tblDefCostCenter LEFT OUTER JOIN " _
            ' & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) " _
            ' & "                                      AS PL2 " _
            ' & "               FROM          dbo.tblVoucher INNER JOIN " _
            ' & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
            ' & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 2) " & IIf(Me.dtpFrom.Checked = True, " AND (Convert(varchar, tblVoucher.Voucher_Date,102) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102))", "") & "  " & IIf(Me.dtpTo.Checked = True, " AND (Convert(Varchar, tblVoucher.Voucher_Date, 102) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ", "") & " " _
            ' & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL2 ON dbo.tblDefCostCenter.CostCenterID = PL2.CostCenterID LEFT OUTER JOIN " _
            ' & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
            ' & "                                      AS PL1 " _
            ' & "              FROM          dbo.tblVoucher INNER JOIN " _
            ' & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
            ' & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 1) " & IIf(Me.dtpFrom.Checked = True, "AND tblVoucher.Voucher_Date >=Convert(Datetime, '" & Me.dtpFrom.Value & "', 102)", "") & "  " & IIf(Me.dtpTo.Checked = True, "AND tblVoucher.Voucher_Date <=Convert(Datetime, '" & Me.dtpTo.Value & "', 102)", "") & " " _
            ' & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL1 ON dbo.tblDefCostCenter.CostCenterID = PL1.CostCenterID LEFT OUTER JOIN " _
            ' & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
            ' & "                                      AS PL3 " _
            ' & "               FROM          dbo.tblVoucher INNER JOIN " _
            ' & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
            ' & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 3) " & IIf(Me.dtpFrom.Checked = True, "AND tblVoucher.Voucher_Date >=Convert(Datetime, '" & Me.dtpFrom.Value & "', 102)", "") & "  " & IIf(Me.dtpTo.Checked = True, "AND tblVoucher.Voucher_Date <=Convert(Datetime, '" & Me.dtpTo.Value & "', 102)", "") & " " _
            ' & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL3 ON dbo.tblDefCostCenter.CostCenterID = PL3.CostCenterID LEFT OUTER JOIN " _
            ' & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
            ' & "                                      AS PL4 " _
            ' & "               FROM          dbo.tblVoucher INNER JOIN " _
            ' & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
            ' & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 4) " & IIf(Me.dtpFrom.Checked = True, "AND tblVoucher.Voucher_Date >=Convert(Datetime, '" & Me.dtpFrom.Value & "', 102)", "") & "  " & IIf(Me.dtpTo.Checked = True, "AND tblVoucher.Voucher_Date <=Convert(Datetime, '" & Me.dtpTo.Value & "', 102)", "") & " " _
            ' & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL4 ON dbo.tblDefCostCenter.CostCenterID = PL4.CostCenterID LEFT OUTER JOIN " _
            ' & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
            ' & "                                      AS PL5 " _
            ' & "               FROM          dbo.tblVoucher INNER JOIN " _
            ' & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
            ' & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 5) " & IIf(Me.dtpFrom.Checked = True, "AND tblVoucher.Voucher_Date >=Convert(Datetime, '" & Me.dtpFrom.Value & "', 102)", "") & "  " & IIf(Me.dtpTo.Checked = True, "AND tblVoucher.Voucher_Date <=Convert(Datetime, '" & Me.dtpTo.Value & "', 102)", "") & " " _
            ' & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL5 ON dbo.tblDefCostCenter.CostCenterID = PL5.CostCenterID LEFT OUTER JOIN " _
            ' & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
            ' & "                                      AS PL6 " _
            ' & "               FROM          dbo.tblVoucher INNER JOIN " _
            ' & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
            ' & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 6) " & IIf(Me.dtpFrom.Checked = True, "AND tblVoucher.Voucher_Date >=Convert(Datetime, '" & Me.dtpFrom.Value & "', 102)", "") & "  " & IIf(Me.dtpTo.Checked = True, "AND tblVoucher.Voucher_Date <=Convert(Datetime, '" & Me.dtpTo.Value & "', 102)", "") & " " _
            ' & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL6 ON dbo.tblDefCostCenter.CostCenterID = PL6.CostCenterID LEFT OUTER JOIN " _
            ' & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
            ' & "                                      AS PL7 " _
            ' & "               FROM          dbo.tblVoucher INNER JOIN " _
            ' & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
            ' & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 7) " & IIf(Me.dtpFrom.Checked = True, "AND tblVoucher.Voucher_Date >=Convert(Datetime, '" & Me.dtpFrom.Value & "', 102)", "") & "  " & IIf(Me.dtpTo.Checked = True, "AND tblVoucher.Voucher_Date <=Convert(Datetime, '" & Me.dtpTo.Value & "', 102)", "") & " " _
            ' & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL7 ON dbo.tblDefCostCenter.CostCenterID = PL7.CostCenterID LEFT OUTER JOIN " _
            ' & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) " _
            ' & "                                      AS PL8 " _
            ' & "               FROM          dbo.tblVoucher INNER JOIN " _
            ' & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
            ' & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
            ' & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 8) " & IIf(Me.dtpFrom.Checked = True, "AND tblVoucher.Voucher_Date >=Convert(Datetime, '" & Me.dtpFrom.Value & "', 102)", "") & "  " & IIf(Me.dtpTo.Checked = True, "AND tblVoucher.Voucher_Date <=Convert(Datetime, '" & Me.dtpTo.Value & "', 102)", "") & " " _
            ' & " GROUP BY dbo.tblVoucherDetail.CostCenterID) PL8 ON dbo.tblDefCostCenter.CostCenterID = PL8.CostCenterID "



            str = "SELECT dbo.tblDefCostCenter.CostCenterID, dbo.tblDefCostCenter.Name, dbo.tblDefCostCenter.CostCenterGroup, ISNULL(PL1.PL1, 0) AS PL1, ISNULL(PL2.PL2,0) AS PL2, ISNULL(PL3.PL3, 0) AS PL3,  ISNULL(PL4.PL4, 0) AS PL4, ISNULL(PL5.PL5, 0) AS PL5, ISNULL(PL6.PL6, 0) AS PL6, ISNULL(PL7.PL7, 0)AS PL7,ISNULL(PL8.PL8, 0) AS PL8, (ISNULL(PL1.PL1, 0)+ISNULL(PL2.PL2,0)+ISNULL(PL3.PL3, 0)+ISNULL(PL4.PL4, 0)+ISNULL(PL5.PL5, 0)+ISNULL(PL6.PL6, 0)+ISNULL(PL7.PL7,0)+ISNULL(PL8.PL8,0)) as SumPL " _
             & " FROM  dbo.tblDefCostCenter LEFT OUTER JOIN " _
             & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) " _
             & "                                      AS PL2 " _
             & "               FROM          dbo.tblVoucher INNER JOIN " _
             & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
             & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 2) " & IIf(Me.dtpFrom.Checked = True, " AND (Convert(varchar, tblVoucher.Voucher_Date,102) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102))", "") & "  " & IIf(Me.dtpTo.Checked = True, " AND (Convert(Varchar, tblVoucher.Voucher_Date, 102) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ", "") & " " _
             & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL2 ON dbo.tblDefCostCenter.CostCenterID = PL2.CostCenterID LEFT OUTER JOIN " _
             & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
             & "                                      AS PL1 " _
             & "              FROM          dbo.tblVoucher INNER JOIN " _
             & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
             & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 1) " & IIf(Me.dtpFrom.Checked = True, " AND (Convert(varchar, tblVoucher.Voucher_Date,102) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102))", "") & "  " & IIf(Me.dtpTo.Checked = True, " AND (Convert(Varchar, tblVoucher.Voucher_Date, 102) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ", "") & " " _
             & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL1 ON dbo.tblDefCostCenter.CostCenterID = PL1.CostCenterID LEFT OUTER JOIN " _
             & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
             & "                                      AS PL3 " _
             & "               FROM          dbo.tblVoucher INNER JOIN " _
             & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
             & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 3) " & IIf(Me.dtpFrom.Checked = True, " AND (Convert(varchar, tblVoucher.Voucher_Date,102) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102))", "") & "  " & IIf(Me.dtpTo.Checked = True, " AND (Convert(Varchar, tblVoucher.Voucher_Date, 102) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ", "") & " " _
             & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL3 ON dbo.tblDefCostCenter.CostCenterID = PL3.CostCenterID LEFT OUTER JOIN " _
             & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
             & "                                      AS PL4 " _
             & "               FROM          dbo.tblVoucher INNER JOIN " _
             & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
             & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 4) " & IIf(Me.dtpFrom.Checked = True, " AND (Convert(varchar, tblVoucher.Voucher_Date,102) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102))", "") & "  " & IIf(Me.dtpTo.Checked = True, " AND (Convert(Varchar, tblVoucher.Voucher_Date, 102) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ", "") & " " _
             & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL4 ON dbo.tblDefCostCenter.CostCenterID = PL4.CostCenterID LEFT OUTER JOIN " _
             & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
             & "                                      AS PL5 " _
             & "               FROM          dbo.tblVoucher INNER JOIN " _
             & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
             & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 5) " & IIf(Me.dtpFrom.Checked = True, " AND (Convert(varchar, tblVoucher.Voucher_Date,102) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102))", "") & "  " & IIf(Me.dtpTo.Checked = True, " AND (Convert(Varchar, tblVoucher.Voucher_Date, 102) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ", "") & " " _
             & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL5 ON dbo.tblDefCostCenter.CostCenterID = PL5.CostCenterID LEFT OUTER JOIN " _
             & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
             & "                                      AS PL6 " _
             & "               FROM          dbo.tblVoucher INNER JOIN " _
             & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
             & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 6) " & IIf(Me.dtpFrom.Checked = True, " AND (Convert(varchar, tblVoucher.Voucher_Date,102) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102))", "") & "  " & IIf(Me.dtpTo.Checked = True, " AND (Convert(Varchar, tblVoucher.Voucher_Date, 102) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ", "") & " " _
             & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL6 ON dbo.tblDefCostCenter.CostCenterID = PL6.CostCenterID LEFT OUTER JOIN " _
             & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0))  " _
             & "                                      AS PL7 " _
             & "               FROM          dbo.tblVoucher INNER JOIN " _
             & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
             & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 7) " & IIf(Me.dtpFrom.Checked = True, " AND (Convert(varchar, tblVoucher.Voucher_Date,102) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102))", "") & "  " & IIf(Me.dtpTo.Checked = True, " AND (Convert(Varchar, tblVoucher.Voucher_Date, 102) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ", "") & " " _
             & "               GROUP BY dbo.tblVoucherDetail.CostCenterID) PL7 ON dbo.tblDefCostCenter.CostCenterID = PL7.CostCenterID LEFT OUTER JOIN " _
             & "             (SELECT     dbo.tblVoucherDetail.CostCenterID, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0) - ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) " _
             & "                                      AS PL8 " _
             & "               FROM          dbo.tblVoucher INNER JOIN " _
             & "                                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id INNER JOIN " _
             & "                                      dbo.tblCOAMainSubSub ON dbo.tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " _
             & " WHERE(dbo.tblCOAMainSubSub.PL_note_id = 8) " & IIf(Me.dtpFrom.Checked = True, " AND (Convert(varchar, tblVoucher.Voucher_Date,102) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102))", "") & "  " & IIf(Me.dtpTo.Checked = True, " AND (Convert(Varchar, tblVoucher.Voucher_Date, 102) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ", "") & " " _
             & " GROUP BY dbo.tblVoucherDetail.CostCenterID) PL8 ON dbo.tblDefCostCenter.CostCenterID = PL8.CostCenterID "
            str += " WHERE " & IIf(Me.chkInActive.Checked = True, " dbo.tblDefCostCenter.Active IN(1,0,NULL)", "dbo.tblDefCostCenter.Active=1") & ""
            Da = New SqlClient.SqlDataAdapter(str, Con)
            Da.Fill(Dt)
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            Dt = Nothing
            Da = Nothing
            Con.Close()
        End Try
    End Function
    Private Sub BindDataGrdPLCostCenter()
        Try
            Me.GrdPLCostCenter.DataSource = Me.GetRecords
            Me.GrdPLCostCenter.RetrieveStructure()
            ApplyGridSetting()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BtnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click
        Try
            BindDataGrdPLCostCenter()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub UiCtrlGridBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.UiCtrlGridBar1.txtGridTitle.Text = "Profit & Loss Cost Center Wise From " & Me.dtpFrom.Value.Date.ToString("dd-MM-yyyy") & " To " & Me.dtpTo.Value.Date.ToString("dd-MM-yyyy") & " "
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub frmRptGrdPLCostCenter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.cmbPeriod.Text = "Current Month"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdPLCostCenter.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdPLCostCenter.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.GrdPLCostCenter.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Cost Center Wise Profit And Loss" & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFrom.Value = Date.Today
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-1)
            Me.dtpTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpTo.Value = Date.Today
        End If
    End Sub
End Class