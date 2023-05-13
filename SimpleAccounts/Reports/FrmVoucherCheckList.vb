'' 27-Jul-2014 Task:2764 Imran Ali Check list Filter By Voucher Type (Ravi)
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal.SQLHelper

Public Class FrmVoucherCheckList


    Private Sub BtnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShow.Click
        Try
            AddRptParam("@fromDate", dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@todate", dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            'Before against task:2764
            'ShowReport("rptVoucherCheckList")
            'Task:2764 Filter through voucher type.
            ShowReport("rptVoucherCheckList", "" & IIf(Me.lstVoucherType.SelectedItems.Length > 1, "{sp_Voucher_Check_List;1.voucher_type} in [" & Me.lstVoucherType.SelectedItems & "]", "") & "")
            'End Task:2764

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FrmVoucherCheckList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.dtpFromDate.Value = Now.AddMonths(-1)
            Me.dtpToDate.Value = Now
            'Task:2764 Filled Voucher Type Listbox 
            FillListBox(Me.lstVoucherType.ListItem, "Select Voucher_Type, Voucher_Type From tblDefVoucherType Order By Sort_Order ASC")
            'End Task:2764
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class