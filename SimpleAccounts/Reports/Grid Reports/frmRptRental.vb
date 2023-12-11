'' 12-Dec-2013 ReqId-914 Imran Ali Rental Report Problem
'' 13-Dec-2013 ReqId-923 Imran Ali Add Column Dispatch C/F And Add 1 day in Date Diff in Rental Repor
'' 28-Dec-2013 R:M6  Imran Ali           Release 2.1.0.0 Bug
Imports SBModel
Public Class frmRptRental

    Private Sub frmRptRental_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    ''' <summary>
    ''' Imran Ali 
    ''' Task agaisnt Request No. 818
    ''' 9-9-2013 to 10-9-2013
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmRptRental_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            FillDropDown(Me.cmbLocation, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order") 'FillCombo 
            Me.cmbLocation.Focus() ' SET Focus Combo Location
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmRptRental)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''ReqId-914 12-Dec-2013 Revised Code
    ''ReqId-923 Added Column of DCF(Dispatch Carry Farword) And 1 day add in DateDiff
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            Dim dblPendingStock As Double = 0D
            Dim dblPendingDisp As Double = 0D
            Dim strDispatchRemarks As String = String.Empty
            Dim dispDate As DateTime

            Dim dblCF As Double = 0D

            Dim ds As New dsRentalData 'Create Object From Dataset
            Dim dr As DataRow 'Declare Data Row Variable

            Dim strSQL As String = String.Empty
            strSQL = "Select Remarks, DispatchDate, SUM(Isnull(Qty,0)) as Qty From DispatchDetailTable INNER JOIN DispatchMasterTable on DispatchMasterTable.DispatchId = DispatchDetailTable.DispatchId  WHERE LEFT(DispatchMasterTable.DispatchNo,2)='DN' AND DispatchDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "  Group By Remarks, DispatchDate"
            Dim dtRecv As New DataTable
            dtRecv = GetDataTable(strSQL) 'Receiving Data

            strSQL = String.Empty
            strSQL = "Select Remarks, ReceivingDate, SUM(ISNULL(Qty,0)) as Qty From ReceivingDetailTable INNER JOIN ReceivingMasterTable On ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId WHERE LEFT(ReceivingNo,3) <> 'Pur' AND ReceivingDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & " Group By Remarks, ReceivingDate"
            Dim dtDisp As New DataTable
            dtDisp = GetDataTable(strSQL) 'Dispatch Data
            Dim intCount As Integer = 0I 'Declare Variable for 2nd Loop 
            Dim intConter As Integer = 0I 'for continue rows of 2 end loop 
            Dim dblDispQty As Double = 0D
            Dim dblRecvQty As Double = 0D
            Dim intRecvCount = 0I 'counter for 1 loop
            Dim intDispCount = 0I 'counter for 2 loop
            For i As Integer = intRecvCount To dtRecv.Rows.Count - 1 'Loop From Receiving Data
                If dblPendingDisp > 0 Then 'If pending dispatch grater than zeror then new row from receiving data
                    dr = ds.Tables(0).NewRow
                    dr(0) = dtRecv.Rows(i).Item("Remarks").ToString 'SET Doc No
                    dr(1) = dtRecv.Rows(i).Item("DispatchDate").ToString 'SET Doc Date
                    dr(2) = Val(dtRecv.Rows(i).Item("Qty").ToString) 'Receiving Qty
                    dr(3) = dblPendingStock 'SET C/F Qty
                    dr(4) = strDispatchRemarks 'SET Dispatch No
                    dr(5) = dispDate 'SET Dispatch Date
                    If Val(dtRecv.Rows(i).Item("Qty").ToString) < dblPendingDisp Then
                        dr(6) = Val(dtRecv.Rows(i).Item("Qty").ToString)
                    Else
                        dr(6) = dblPendingDisp
                    End If
                    dr(7) = Val(dtRecv.Rows(i).Item("Qty").ToString) - dblPendingDisp
                    dblPendingStock = Val(dtRecv.Rows(i).Item("Qty").ToString) - dblPendingDisp
                    dr(8) = 0 'dblPendingDisp
                    dr(9) = dblDispQty 'dblPendingDisp
                    dr(11) = DateDiff(DateInterval.Day, CDate(dtRecv.Rows(i).Item("DispatchDate").ToString), CDate(dispDate).AddDays(1))
                    ds.Tables(0).Rows.Add(dr)
                    dblPendingDisp = 0
                End If
                '' Second Loop for dispatch
                'R:M6 Check Exist Dispatch Rows
                If dtDisp.Rows.Count > 0 Then
                    For j As Integer = intDispCount To dtDisp.Rows.Count - 1 'Loop From Dispatch Data
                        If dblPendingStock > 0 Then
                            dr = ds.Tables(0).NewRow
                            dr(0) = dtRecv.Rows(i).Item("Remarks").ToString ' Bowser Receiving No
                            dr(1) = dtRecv.Rows(i).Item("DispatchDate").ToString 'Arrival Date
                            dr(2) = Val(dtRecv.Rows(i).Item("Qty").ToString) 'Receiving Qty
                            dr(3) = dblPendingStock 'Val(dtDisp.Rows(i).Item("CF").ToString)
                            dr(4) = dtDisp.Rows(j).Item("Remarks").ToString 'SET Dispatch No
                            dr(5) = dtDisp.Rows(j).Item("ReceivingDate").ToString 'SET Dispatch Date
                            strDispatchRemarks = dtDisp.Rows(j).Item("Remarks").ToString
                            dispDate = CDate(dtDisp.Rows(j).Item("ReceivingDate").ToString)
                            If (dblPendingStock) < Val(dtDisp.Rows(j).Item("Qty").ToString) Then
                                dr(6) = dblPendingStock '(dblPendingDisp + Val(dtDisp.Rows(j).Item("Qty").ToString))
                                'dblPendingStock = 0 'Val(dtRecv.Rows(i).Item("Qty").ToString) - (Val(dtDisp.Rows(j).Item("Qty").ToString) + dblPendingDisp)
                                dblPendingDisp = Val(dtDisp.Rows(j).Item("Qty").ToString) - dblPendingStock
                                dblPendingStock = 0
                            Else
                                dr(6) = Val(dtDisp.Rows(j).Item("Qty").ToString)
                                dblPendingDisp = 0 '((dblPendingDisp + Val(dtDisp.Rows(j).Item("Qty").ToString)) - Val(dtRecv.Rows(i).Item("Qty").ToString))
                                dblPendingStock = dblPendingDisp - Val(dtDisp.Rows(j).Item("Qty").ToString)
                            End If
                            dr(7) = dblPendingStock
                            dr(8) = dblPendingDisp
                            dr(9) = Val(dtDisp.Rows(j).Item("Qty").ToString)
                            dblDispQty = Val(dtDisp.Rows(j).Item("Qty").ToString)
                            dr(11) = DateDiff(DateInterval.Day, CDate(dtRecv.Rows(i).Item("DispatchDate").ToString), CDate(dispDate).AddDays(1))
                            dblPendingStock = 0
                            ds.Tables(0).Rows.Add(dr)
                            intDispCount += 1 'Set Count For Next Row Of 2nd Loop
                            Exit For
                        End If

                        dr = ds.Tables(0).NewRow
                        dr(0) = dtRecv.Rows(i).Item("Remarks").ToString 'SET Doc No
                        dr(1) = dtRecv.Rows(i).Item("DispatchDate").ToString 'SET Doc Date
                        dr(2) = Val(dtRecv.Rows(i).Item("Qty").ToString)
                        dr(3) = 0 'dblPendingStock  'SET C/F Qty
                        dr(4) = dtDisp.Rows(j).Item("Remarks").ToString 'SET Dispatch No
                        dr(5) = dtDisp.Rows(j).Item("ReceivingDate").ToString 'SET Dispatch Date
                        strDispatchRemarks = dtDisp.Rows(j).Item("Remarks").ToString
                        dispDate = CDate(dtDisp.Rows(j).Item("ReceivingDate").ToString)
                        If dblPendingDisp = 0 Then 'If Pending Dispatch then bellow execute code other wise dispatch forward to next row
                            If Val(dtRecv.Rows(i).Item("Qty").ToString) + Val(dr(3)) < Val(dtDisp.Rows(j).Item("Qty").ToString) Then
                                dr(6) = Val(dtRecv.Rows(i).Item("Qty").ToString)
                                dblPendingStock = 0
                                dblPendingDisp = Val(dtDisp.Rows(j).Item("Qty").ToString) - Val(dtRecv.Rows(i).Item("Qty").ToString)
                            Else
                                dr(6) = Val(dtDisp.Rows(j).Item("Qty").ToString) - (Val(dtRecv.Rows(i).Item("Qty").ToString) - Val(dtRecv.Rows(i).Item("Qty").ToString))
                                dblPendingStock = Val(dtRecv.Rows(i).Item("Qty").ToString) - Val(dtDisp.Rows(j).Item("Qty").ToString)
                                dblPendingDisp = 0
                            End If
                        Else ' if Pending Dispatch Grater Than Zero
                            If (Val(dtDisp.Rows(j).Item("Qty").ToString) + dblPendingDisp) < Val(dtRecv.Rows(i).Item("Qty").ToString) Then
                                dr(6) = (dblPendingDisp + Val(dtDisp.Rows(j).Item("Qty").ToString))
                                dblPendingStock = Val(dtRecv.Rows(i).Item("Qty").ToString) - (Val(dtDisp.Rows(j).Item("Qty").ToString) + dblPendingDisp)
                                dblPendingDisp = 0
                            Else
                                dr(6) = Val(dtRecv.Rows(i).Item("Qty").ToString)
                                dblPendingDisp = ((dblPendingDisp + Val(dtDisp.Rows(j).Item("Qty").ToString)) - Val(dtRecv.Rows(i).Item("Qty").ToString))
                                dblPendingStock = 0
                            End If
                        End If
                        dr(7) = dblPendingStock
                        dr(8) = dblPendingDisp
                        dr(9) = Val(dtDisp.Rows(j).Item("Qty").ToString)
                        dblDispQty = Val(dtDisp.Rows(j).Item("Qty").ToString)
                        dr(11) = DateDiff(DateInterval.Day, CDate(dtRecv.Rows(i).Item("DispatchDate").ToString), CDate(dtDisp.Rows(j).Item("ReceivingDate").ToString).AddDays(1))
                        ds.Tables(0).Rows.Add(dr)
                        intDispCount += 1
                        '' If pending dispatch grater than zeror then return next loop from 1st
                        If dblPendingDisp > 0 Then
                            Exit For
                        End If
                    Next

                    intRecvCount += 1
                    Dim dr12 As DataRow = ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1) 'Last Row Data if receiving record grater than dispatch record
                    If intRecvCount > intDispCount Then
                        If dblPendingStock > 0 Then
                            dr = ds.Tables(0).NewRow
                            dr(0) = dr12(0).ToString ' Bowser Receiving No
                            dr(1) = dr12(1).ToString 'Arrival Date
                            dr(2) = Val(dr12(2).ToString) 'Receiving Qty
                            dr(3) = dblPendingStock 'Val(dtDisp.Rows(i).Item("CF").ToString)
                            dr(4) = String.Empty  'SET Dispatch No
                            dr(5) = Now.Date  'SET Dispatch Date
                            dr(6) = 0
                            dr(7) = dblPendingStock
                            dr(8) = dblPendingDisp
                            dr(9) = 0
                            dblDispQty = 0
                            dr(11) = DateDiff(DateInterval.Day, CDate(dr12(1).ToString), CDate(dispDate).AddDays(1))
                            dblPendingStock = 0
                            ds.Tables(0).Rows.Add(dr)

                            dr = ds.Tables(0).NewRow
                            dr(0) = dtRecv.Rows(i).Item("Remarks").ToString 'SET Doc No
                            dr(1) = dtRecv.Rows(i).Item("DispatchDate").ToString 'SET Doc Date
                            dr(2) = Val(dtRecv.Rows(i).Item("Qty").ToString)
                            dr(3) = 0 'dblPendingStock  'SET C/F Qty
                            dr(4) = String.Empty 'dtDisp.Rows(j).Item("Remarks").ToString 'SET Dispatch No
                            dr(5) = Now 'dtDisp.Rows(j).Item("ReceivingDate").ToString 'SET Dispatch Date
                            dr(6) = 0
                            dr(7) = dblPendingDisp
                            dr(8) = dblPendingStock
                            dr(9) = 0 'Val(dtDisp.Rows(j).Item("Qty").ToString)
                            dblDispQty = 0 'Val(dtDisp.Rows(j).Item("Qty").ToString)
                            dr(11) = DateDiff(DateInterval.Day, CDate(dtRecv.Rows(i).Item("DispatchDate").ToString), CDate(Now).AddDays(1))
                            ds.Tables(0).Rows.Add(dr)
                        Else
                            dr = ds.Tables(0).NewRow
                            dr(0) = dtRecv.Rows(i).Item("Remarks").ToString 'SET Doc No
                            dr(1) = dtRecv.Rows(i).Item("DispatchDate").ToString 'SET Doc Date
                            dr(2) = Val(dtRecv.Rows(i).Item("Qty").ToString)
                            dr(3) = 0 'dblPendingStock  'SET C/F Qty
                            dr(4) = String.Empty 'dtDisp.Rows(j).Item("Remarks").ToString 'SET Dispatch No
                            dr(5) = Now 'dtDisp.Rows(j).Item("ReceivingDate").ToString 'SET Dispatch Date
                            'If dblPendingDisp = 0 Then 'If Pending Dispatch then bellow execute code other wise dispatch forward to next row
                            '    If Val(dtRecv.Rows(i).Item("Qty").ToString) + Val(dr(3)) < Val(dtDisp.Rows(j).Item("Qty").ToString) Then
                            '        dr(6) = Val(dtRecv.Rows(i).Item("Qty").ToString)
                            '        dblPendingStock = 0
                            '        dblPendingDisp = Val(dtDisp.Rows(j).Item("Qty").ToString) - Val(dtRecv.Rows(i).Item("Qty").ToString)
                            '    Else
                            '        dr(6) = Val(dtDisp.Rows(j).Item("Qty").ToString) - (Val(dtRecv.Rows(i).Item("Qty").ToString) - Val(dtRecv.Rows(i).Item("Qty").ToString))
                            '        dblPendingStock = Val(dtRecv.Rows(i).Item("Qty").ToString) - Val(dtDisp.Rows(j).Item("Qty").ToString)
                            '        dblPendingDisp = 0
                            '    End If
                            'Else ' if Pending Dispatch Grater Than Zero
                            '    If (Val(dtDisp.Rows(j).Item("Qty").ToString) + dblPendingDisp) < Val(dtRecv.Rows(i).Item("Qty").ToString) Then
                            '        dr(6) = (dblPendingDisp + Val(dtDisp.Rows(j).Item("Qty").ToString))
                            '        dblPendingStock = Val(dtRecv.Rows(i).Item("Qty").ToString) - (Val(dtDisp.Rows(j).Item("Qty").ToString) + dblPendingDisp)
                            '        dblPendingDisp = 0
                            '    Else
                            '        dr(6) = Val(dtRecv.Rows(i).Item("Qty").ToString)
                            '        dblPendingDisp = ((dblPendingDisp + Val(dtDisp.Rows(j).Item("Qty").ToString)) - Val(dtRecv.Rows(i).Item("Qty").ToString))
                            '        dblPendingStock = 0
                            '    End If
                            'End If
                            dr(6) = 0
                            dr(7) = dblPendingDisp
                            dr(8) = dblPendingStock
                            dr(9) = 0 'Val(dtDisp.Rows(j).Item("Qty").ToString)
                            dblDispQty = 0 'Val(dtDisp.Rows(j).Item("Qty").ToString)
                            dr(11) = DateDiff(DateInterval.Day, CDate(dtRecv.Rows(i).Item("DispatchDate").ToString), CDate(Now).AddDays(1))
                            ds.Tables(0).Rows.Add(dr)
                        End If
                    End If
                Else 'R:M6 If Not Exist Dispath 

                    dr = ds.Tables(0).NewRow
                    dr(0) = dtRecv.Rows(i).Item("Remarks").ToString 'SET Doc No
                    dr(1) = dtRecv.Rows(i).Item("DispatchDate").ToString 'SET Doc Date
                    dr(2) = Val(dtRecv.Rows(i).Item("Qty").ToString) 'Receiving Qty
                    dr(3) = 0 'SET C/F Qty
                    dr(4) = 0 'SET Dispatch No
                    dr(5) = Now 'SET Dispatch Date
                    'If Val(dtRecv.Rows(i).Item("Qty").ToString) < dblPendingDisp Then
                    '    dr(6) = Val(dtRecv.Rows(i).Item("Qty").ToString)
                    'Else
                    '    dr(6) = dblPendingDisp
                    'End If
                    dr(6) = 0
                    dr(7) = 0 'Val(dtRecv.Rows(i).Item("Qty").ToString) - dblPendingDisp
                    'dblPendingStock = Val(dtRecv.Rows(i).Item("Qty").ToString) - dblPendingDisp
                    dr(8) = 0 'dblPendingDisp
                    dr(9) = 0 'dblDispQty 'dblPendingDisp
                    dr(11) = 0 'DateDiff(DateInterval.Day, CDate(dtRecv.Rows(i).Item("DispatchDate").ToString), CDate(dispDate).AddDays(1))
                    ds.Tables(0).Rows.Add(dr)
                    dblPendingDisp = 0

                End If 'End R:M6
            Next
            Dim dblBalanceQty As Double = 0D
            Dim intCnt As Integer = 0
            Dim dblBalance As Double = 0D
            'For SET Running Balance
            For Each r As DataRow In ds.Tables(0).Rows
                'r.Item("Balance") = Val(r.Item("Dispatch").ToString) - Val(r.Item("Out_Qty").ToString)

                r.BeginEdit()
                If intCnt = 0 Or dblBalance = 0 Then
                    r.Item("Balance") = Val(r.Item("Dispatch").ToString) - Val(r.Item("Out_Qty").ToString)
                    dblBalance = r.Item("Balance")
                    If Not intCnt >= 0 Then
                        dblBalanceQty += Val(r.Item("Out_Qty").ToString)
                    Else
                        dblBalanceQty = Val(r.Item("Out_Qty").ToString)
                    End If
                    ''dblBalanceQty
                Else
                    dblBalanceQty += Val(r.Item("Out_Qty").ToString)
                    r.Item("Balance") = Val(r.Item("Dispatch").ToString) - dblBalanceQty
                    dblBalance = Val(r.Item("Dispatch").ToString) - dblBalanceQty
                End If
                r.EndEdit()
                intCnt += 1
            Next

            ds.Tables(0).Columns("Amount").Expression = "(Rate*Days)*Out_Qty" 'SET Default Expression
            ds.Tables(0).AcceptChanges() 'SET Update Data in DataTable
            Me.grd.DataSource = ds.Tables(0) 'SET Data Source Of DataSet
            ApplyGridSetting() 'Call Method For Grid Setting
            Me.grd.AutoSizeColumns() 'Resizing Columns
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbLocation.SelectedIndex
            FillDropDown(Me.cmbLocation, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order")
            Me.cmbLocation.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If c.Index <> 11 Then
                    c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub grd_ColumnHeaderClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnHeaderClick
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            Dim i As Integer = 0
            If Not e.Column.Key = "Rate" Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                r.BeginEdit()
                If i > 0 Then
                    Me.grd.GetRows(i).Cells("Rate").Value = Val(Me.grd.GetRows(0).Cells("Rate").Value.ToString)
                End If
                r.EndEdit()
                i += 1
            Next


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "LPG Storage Billing For: " & Me.cmbLocation.Text & ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class