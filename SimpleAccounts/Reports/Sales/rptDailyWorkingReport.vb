''03-Jan-2014      Task:2409  Imran Ali           Net amount column should be added in sale report of daily working report.
''05-Mar-2014   TasK:2459  Imran Ali   Quantity Field on Daily Working Report
''14-Mar-2014 Task:2492 Imran Ali Customer And Vendor Advance Adjustment in Daily Working Report
''27-Mar-2014 TASK:2520 Imran Ali   daily working report JV problem.
''31-Mar-2014 TASK:2531 Imran ali Miscellaneous adjustment balance effect on current balance  in daily working report''' <remarks></remarks>
'Task No 2597 Mughees 28-4-2014 Update Query For Unposted Voucher
''12-Sep-2014 Task:2840 Imran Ali Zero Balance Hide In Purchase Daily Working Report
Public Class rptDailyWorkingReport

    Implements IGeneral
    Enum EnumGrid
        Name
        Sector
        Type
        Dispatch
        DispQty 'Task:2459 Added Index
        Ret
        RetQty 'Task:2459 Added Index
        RetPer
        Unsold
        UnsoldQty 'Task:2459 Added Index
        PreviousBalance
        Offtake
        TaxAmount 'Task:2459 Added Index
        Payable
        S_Ret
        MC
        Sampling
        Petrol
        Tooltax
        Adjsmt
        Cashpaid
        ContDays
        MiscAdj 'Task:2459 Added Index
        NetSales ' Task:2409 Added Index Net Sales
        ShortCash
        AddCash ' Task:2409 Added Index Net Sales
        'SaleManAccess
        CurrentBalance

    End Enum
    Dim IsOpenForm As Boolean = False

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.grdRcords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdRcords.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdRcords.RecordNavigator = True
            Me.grdRcords.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007


            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdRcords.RootTable.Columns

                If col.Index <> EnumGrid.Name AndAlso col.Index <> EnumGrid.Sector AndAlso col.Index <> EnumGrid.Type Then
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    If Not col.Index = EnumGrid.RetPer Then col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    col.FormatString = "N0" 'String.Empty
                    col.TotalFormatString = "N0" 'String.Empty
                End If
            Next

            Me.grdRcords.RootTable.Groups.Add(Me.grdRcords.RootTable.Columns(EnumGrid.Type), Janus.Windows.GridEX.SortOrder.Ascending)
            Me.grdRcords.RootTable.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.grdRcords.RootTable.Groups(0).Expand()

            Me.grdRcords.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.grdRcords.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
            Me.grdRcords.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
            Me.grdRcords.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains

            Me.grdRcords.RootTable.Columns(EnumGrid.Type).Caption = "Customer Type"
            Me.grdRcords.RootTable.Columns(EnumGrid.ContDays).Caption = "Days"
            Me.grdRcords.AutoSizeColumns()

            'Try
            '    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
            '    Me.grdRcords.LoadLayoutFile(fs)
            '    fs.Close()
            '    fs.Dispose()
            'Catch ex As Exception
            'End Try
            'Task:2409 Change Index Position 
            Me.grdRcords.RootTable.Columns(EnumGrid.NetSales).Position = Me.grdRcords.RootTable.Columns(EnumGrid.Cashpaid).Position
            'End Task:2409
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

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

    Private Sub btnGenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateReport.Click
        Try
            Dim strSQL As New System.Text.StringBuilder


            ''"     select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            ''" isnull(ret.ret,0)as Ret ,   (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ," & _
            ''" isnull(Damage.Dmg,0) as Unsold , isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - isnull(ret.ret,0) as [Offtake], " & _
            ''" isnull(PrevBal.pbal,0) + isnull(dispatch.dispatch,0) - isnull(ret.ret,0)  as Payable,  0 as S_ret,  isnull(dispatch.mc,0) as MC, isnull(dispatch.Sampling,0) as Sampling, " & _
            ''" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt, " & _
            ''" isnull(cs.CashPaid,0) as [Cash Paid] ")
            ''" from vwcoadetail  left outer join  " & _
            ''" (SELECT     SUM(SalesDetailTable.Qty * SalesDetailTable.Price) AS dispatch, CustomerCode, sum((SalesDetailTable.CurrentPrice - price)* SalesDetailTable.Qty) as MC,  " & _
            ''" sum(isnull(SampleQty,0)) as Sampling, isnull(Adjustment,0) as Adjsmt, " & _
            ''" isnull(FuelExpense,0) as Petrol, isnull(otherExpense,0) as Tooltax, count(distinct SalesMasterTable.SalesDate) as Days " & _
            ''" FROM         SalesDetailTable INNER JOIN " & _
            ''" SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId " & _
            ''" WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "') " & _
            ''" GROUP BY CustomerCode, isnull(Adjustment,0) ,isnull(FuelExpense,0) , isnull(otherExpense,0) ) as dispatch on vwcoadetail.coa_detail_id = dispatch.CustomerCode left outer join " & _
            ''" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Ret, CustomerCode " & _
            ''" FROM         SalesReturnMasterTable INNER JOIN " & _
            ''" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId   inner join " & _
            ''" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            ''" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "') " & _
            ''" and isnull(tbldeflocation.location_type,'') <> 'Damage'	")
            ''" GROUP BY CustomerCode) as Ret      on vwcoadetail.coa_detail_id= ret.CustomerCode left outer join " & _
            ''" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Dmg, CustomerCode " & _
            ''" FROM         SalesReturnMasterTable INNER JOIN " & _
            ''" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join " & _
            ''" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            ''" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "') " & _
            ''" and isnull(tbldeflocation.location_type,'') = 'Damage'	 ")
            ''" GROUP BY CustomerCode) as Damage  on vwcoadetail.coa_detail_id = Damage.CustomerCode left outer join " & _
            ''" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid" & _
            ''" FROM         tblVoucher INNER JOIN " & _
            ''" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            ''" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <='" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "' AND  " & _
            ''" (tblVoucherDetail.credit_amount > 0) " & _
            ''" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid " & _
            ''" left outer join tblcustomer on tblcustomer.AccountID = vwcoadetail.coa_detail_id")
            ''" left outer join tbllistterritory on tbllistterritory.territoryID = tblcustomer.Territory" & _
            ''" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblcustomer.customertypes  left outer join ")
            ''" (SELECT    sum(tblVoucherDetail.debit_amount) -   SUM(tblVoucherDetail.credit_amount) AS PBal, tblVoucherDetail.coa_detail_id AS accid" & _
            ''" FROM         tblVoucher INNER JOIN " & _
            ''" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            ''" WHERE     ( convert(datetime , convert(varchar, left(tblVoucher.voucher_date,11),102),102)   < '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' ) AND" & _
            ''" (tblVoucherDetail.credit_amount > 0) " & _
            ''" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid ")


            '" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            '" isnull(ret.ret,0)as Ret ,   (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ," & _
            '" isnull(Damage.Dmg,0) as Unsold , isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - isnull(ret.ret,0) as [Offtake], " & _
            '" isnull(PrevBal.pbal,0) + isnull(dispatch.dispatch,0) - isnull(ret.ret,0)  as Payable,  0 as S_ret,  isnull(dispatch.mc,0) as MC, isnull(dispatch.Sampling,0) as Sampling, " & _
            '" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt, " & _
            '" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days]")
            '" from vwcoadetail  left outer join  " & _
            '" (SELECT      SUM(SalesDetailTable.dispatch) as dispatch, CustomerCode, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, COUNT(DISTINCT SalesMasterTable.SalesDate) AS Days ")
            '" FROM        (SELECT     SalesId, SUM(Qty * Price) AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling FROM         dbo.SalesDetailTable GROUP BY SalesId) as SalesDetailTable INNER JOIN " & _
            '" SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId " & _
            '" WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" GROUP BY CustomerCode ) as dispatch on vwcoadetail.coa_detail_id = dispatch.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Ret, CustomerCode " & _
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId   inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" and isnull(tbldeflocation.location_type,'') <> 'Damage'	")
            '" GROUP BY CustomerCode) as Ret      on vwcoadetail.coa_detail_id= ret.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Dmg, CustomerCode " & _
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" and isnull(tbldeflocation.location_type,'') = 'Damage'	 ")
            '" GROUP BY CustomerCode) as Damage  on vwcoadetail.coa_detail_id = Damage.CustomerCode left outer join " & _
            '" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM         tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND " & _
            '" (tblVoucherDetail.credit_amount > 0) " & _
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid " & _
            '" left outer join tblcustomer on tblcustomer.AccountID = vwcoadetail.coa_detail_id")
            '" left outer join tbllistterritory on tbllistterritory.territoryID = tblcustomer.Territory" & _
            '" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblcustomer.customertypes  left outer join ")
            '" (SELECT    sum(tblVoucherDetail.debit_amount) -   SUM(tblVoucherDetail.credit_amount) AS PBal, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM         tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(tblVoucher.voucher_date,11),102),102)   < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND" & _
            '" (tblVoucherDetail.credit_amount > 0) " & _
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid " & IIf(Me.rbtall.Checked = True, "", " where  vwcoadetail.account_type='Customer'") & "")

            '" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            '" isnull(ret.ret,0)as Ret,    (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ," & _
            '" isnull(Damage.Dmg,0) as Unsold, isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Damage.Dmg,0)) as [Offtake], " & _
            '" isnull(PrevBal.pbal,0) + (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Damage.Dmg,0)))  as Payable,  0 as S_ret,  isnull(dispatch.mc,0) as MC, isnull(dispatch.Sampling,0) as Sampling, " & _
            '" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt, " & _
            '" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days]")
            '" from vwcoadetail  left outer join  " & _
            '" (SELECT SUM(SalesDetailTable.dispatch) as dispatch, CustomerCode, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, COUNT(DISTINCT SalesMasterTable.SalesDate) AS Days ")
            '" FROM  (SELECT     SalesId, SUM(Qty * CurrentPrice) AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling FROM         dbo.SalesDetailTable GROUP BY SalesId) as SalesDetailTable INNER JOIN " & _
            '" SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId " & _
            '" WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" GROUP BY CustomerCode ) as dispatch on vwcoadetail.coa_detail_id = dispatch.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Ret, CustomerCode " & _
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId   inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" and isnull(tbldeflocation.location_type,'') <> 'Damage'	")
            '" GROUP BY CustomerCode) as Ret      on vwcoadetail.coa_detail_id= ret.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Dmg, CustomerCode " & _
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" and isnull(tbldeflocation.location_type,'') = 'Damage'	 ")
            '" GROUP BY CustomerCode) as Damage  on vwcoadetail.coa_detail_id = Damage.CustomerCode left outer join " & _
            '" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM         tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND " & _
            '" (tblVoucherDetail.credit_amount > 0) " & _
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid " & _
            '" left outer join tblcustomer on tblcustomer.AccountID = vwcoadetail.coa_detail_id")
            '" left outer join tbllistterritory on tbllistterritory.territoryID = tblcustomer.Territory" & _
            '" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblcustomer.customertypes  left outer join ")
            '" (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM    tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE  (convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid " & IIf(Me.rbtall.Checked = True, "", " where  vwcoadetail.account_type='Customer'") & "")



            '" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            '" isnull(ret.ret,0)as Ret,  (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ," & _
            '" isnull(Unsold.Dmg,0) as Unsold, isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake], " & _
            '" isnull(PrevBal.pbal,0) + (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret,  isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0)) as MC, isnull(dispatch.Sampling,0) as Sampling, " & _
            '" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt, " & _
            '" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days]")
            '" from vwcoadetail  left outer join  " & _
            '" (SELECT SUM(SalesDetailTable.dispatch) as dispatch, CustomerCode, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, Count(Distinct SalesDate) as Days ")
            '" FROM  (SELECT  SalesId, SUM(Qty * CurrentPrice) AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling FROM dbo.SalesDetailTable GROUP BY SalesId) as SalesDetailTable INNER JOIN " & _
            '" SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId ")
            '" WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" GROUP BY CustomerCode ) as dispatch on vwcoadetail.coa_detail_id = dispatch.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.CurrentPrice) AS Ret, CustomerCode, Sum(IsNull(SalesReturnDetailTable.Qty,0)) as ReturnQty, SUM((SalesReturnDetailTable.CurrentPrice - SalesReturnDetailTable.Price) * SalesReturnDetailTable.Qty) as ReturnMC ")
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId   inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" and isnull(tbldeflocation.location_type,'') = 'Damage'	")
            '" GROUP BY CustomerCode) as Ret      on vwcoadetail.coa_detail_id= ret.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.CurrentPrice) AS Dmg, CustomerCode, Sum(IsNull(SalesReturnDetailTable.Qty,0)) as UnsoldQty, SUM((SalesReturnDetailTable.CurrentPrice - SalesReturnDetailTable.Price) * SalesReturnDetailTable.Qty) as UnsoldMC " & _
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" and isnull(tbldeflocation.location_type,'') <> 'Damage'	 ")
            '" GROUP BY CustomerCode) as Unsold  on vwcoadetail.coa_detail_id = Unsold.CustomerCode left outer join " & _
            '" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM         tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND " & _
            '" (tblVoucherDetail.credit_amount > 0) " & _
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid " & _
            '" left outer join tblcustomer on tblcustomer.AccountID = vwcoadetail.coa_detail_id")
            '" left outer join tbllistterritory on tbllistterritory.territoryID = tblcustomer.Territory" & _
            '" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblcustomer.customertypes  left outer join ")
            '" (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM    tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE  (convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid WHERE vwcoadetail.Active=1 " & IIf(Me.rbtall.Checked = True, "", " AND vwcoadetail.account_type='Customer'") & "")




            '"     select CustomerName as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            '" isnull(ret.ret,0)as Ret ,   (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ," & _
            '" isnull(Damage.Dmg,0) as Unsold , isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - isnull(ret.ret,0) as [Offtake], " & _
            '" isnull(PrevBal.pbal,0) + isnull(dispatch.dispatch,0) - isnull(ret.ret,0)  as Payable,  0 as S_ret,  isnull(dispatch.mc,0) as MC, isnull(dispatch.Sampling,0) as Sampling, " & _
            '" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt, " & _
            '" isnull(cs.CashPaid,0) as [Cash Paid] ")
            '" from tblcustomer  left outer join  " & _
            '" (SELECT     SUM(SalesDetailTable.Qty * SalesDetailTable.Price) AS dispatch, CustomerCode, sum((SalesDetailTable.CurrentPrice - price)* SalesDetailTable.Qty) as MC,  " & _
            '" sum(isnull(SampleQty,0)) as Sampling, sum(isnull(Adjustment,0)) as Adjsmt, " & _
            '" sum(isnull(FuelExpense,0)) as Petrol, sum(isnull(otherExpense,0)) as Tooltax, count(SalesMasterTable.SalesId) as Days " & _
            '" FROM         SalesDetailTable INNER JOIN " & _
            '" SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId " & _
            '" WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "') " & _
            '" GROUP BY CustomerCode) as dispatch on tblcustomer.accountid = dispatch.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Ret, CustomerCode " & _
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId " & _
            '" WHERE     ( convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "') " & _
            '" GROUP BY CustomerCode) as Ret      on tblcustomer.accountid = ret.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Dmg, CustomerCode " & _
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnMasterTable.locationID  " & _
            '" WHERE     ( convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "') " & _
            '" and tbldeflocation.location_type = 'Damage'	 ")
            '" GROUP BY CustomerCode) as Damage  on tblcustomer.accountid = Damage.CustomerCode left outer join " & _
            '" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM         tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE     (convert(varchar, tblVoucher.voucher_date,102)  BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "') AND  " & _
            '" (tblVoucherDetail.credit_amount > 0) " & _
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on tblcustomer.accountid = cs.Accid " & _
            '" left outer join tbllistterritory on tbllistterritory.territoryID = tblcustomer.Territory" & _
            '" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblcustomer.customertypes  left outer join ")
            '"  (SELECT    sum(tblVoucherDetail.debit_amount) -   SUM(tblVoucherDetail.credit_amount) AS PBal, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM         tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE     (convert(varchar, tblVoucher.voucher_date,102) < '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' ) AND" & _
            '" (tblVoucherDetail.credit_amount > 0) " & _
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on tblcustomer.accountid = PrevBal.accid ")

            'Dim dt As DataTable = GetDataTable(strSQL.ToString())

            'dt.Columns.Add("ShortCash", GetType(System.Double))
            ''dt.Columns.Add("SalesManAccess", GetType(System.Double))
            'dt.Columns.Add("CurrBal", GetType(System.Double))
            'dt.Columns(EnumGrid.ShortCash).Expression = "((S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid])-[Offtake])"
            ''dt.Columns(EnumGrid.SaleManAccess).Expression = "(([Cash Paid])-([Offtake]-[Unsold]-MC))"
            'dt.Columns(EnumGrid.CurrentBalance).Expression = "(Payable - (S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid]))"

            Dim dt As DataTable = GetData()
            Me.grdRcords.DataSource = dt
            Me.grdRcords.RetrieveStructure()
            Me.ApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub rptDailyWorkingReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Try
    '        GetLayout()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Public Function GetData() As DataTable
        Try

            'Before against task:2409
            'Dim strSQL As New System.Text.StringBuilder
            '" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            '" isnull(ret.ret,0)as Ret,  (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ," & _
            '" isnull(Unsold.Dmg,0) as Unsold, isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake], " & _
            '" isnull(PrevBal.pbal,0) + (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret,  isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0)) as MC, isnull(dispatch.Sampling,0) as Sampling, " & _
            '" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt, " & _
            '" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days]")
            '" from vwcoadetail  left outer join  " & _
            '" (SELECT SUM(SalesDetailTable.dispatch) as dispatch, CustomerCode, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, Count(Distinct SalesDate) as Days ")
            '" FROM  (SELECT  SalesId, SUM(Qty * CurrentPrice) AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling FROM dbo.SalesDetailTable GROUP BY SalesId) as SalesDetailTable INNER JOIN " & _
            '" SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId ")
            '" WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" GROUP BY CustomerCode ) as dispatch on vwcoadetail.coa_detail_id = dispatch.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.CurrentPrice) AS Ret, CustomerCode, Sum(IsNull(SalesReturnDetailTable.Qty,0)) as ReturnQty, SUM((SalesReturnDetailTable.CurrentPrice - SalesReturnDetailTable.Price) * SalesReturnDetailTable.Qty) as ReturnMC ")
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId   inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" and isnull(tbldeflocation.location_type,'') = 'Damage'	")
            '" GROUP BY CustomerCode) as Ret      on vwcoadetail.coa_detail_id= ret.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.CurrentPrice) AS Dmg, CustomerCode, Sum(IsNull(SalesReturnDetailTable.Qty,0)) as UnsoldQty, SUM((SalesReturnDetailTable.CurrentPrice - SalesReturnDetailTable.Price) * SalesReturnDetailTable.Qty) as UnsoldMC " & _
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" and isnull(tbldeflocation.location_type,'') <> 'Damage'	 ")
            '" GROUP BY CustomerCode) as Unsold  on vwcoadetail.coa_detail_id = Unsold.CustomerCode left outer join " & _
            '" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM         tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND " & _
            '" (tblVoucherDetail.credit_amount > 0) " & _
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid " & _
            '" left outer join tblcustomer on tblcustomer.AccountID = vwcoadetail.coa_detail_id")
            '" left outer join tbllistterritory on tbllistterritory.territoryID = tblcustomer.Territory" & _
            '" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblcustomer.customertypes  left outer join ")
            '" (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM    tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE  (convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid WHERE vwcoadetail.Active=1 " & IIf(Me.rbtall.Checked = True, "", " AND vwcoadetail.account_type='Customer'") & "")
            'Before against task:2459
            'Task:2409 Zero Return Column Calc Problem
            'Dim strSQL As New System.Text.StringBuilder
            '" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            '" isnull(ret.ret,0)as Ret,  Case When isnull(ret.ret,0) > 0 Then (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) Else 0 End as [Ret%] ," & _
            '" isnull(Unsold.Dmg,0) as Unsold, isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake], " & _
            '" isnull(PrevBal.pbal,0) + (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret,  isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0)) as MC, isnull(dispatch.Sampling,0) as Sampling, " & _
            '" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt, " & _
            '" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days]")
            '" from vwcoadetail  left outer join  " & _
            '" (SELECT SUM(SalesDetailTable.dispatch) as dispatch, CustomerCode, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, Count(Distinct SalesDate) as Days ")
            '" FROM  (SELECT  SalesId, SUM(Qty * CurrentPrice) AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling FROM dbo.SalesDetailTable GROUP BY SalesId) as SalesDetailTable INNER JOIN " & _
            '" SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId ")
            '" WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" GROUP BY CustomerCode ) as dispatch on vwcoadetail.coa_detail_id = dispatch.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.CurrentPrice) AS Ret, CustomerCode, Sum(IsNull(SalesReturnDetailTable.Qty,0)) as ReturnQty, SUM((SalesReturnDetailTable.CurrentPrice - SalesReturnDetailTable.Price) * SalesReturnDetailTable.Qty) as ReturnMC ")
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId   inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" and isnull(tbldeflocation.location_type,'') = 'Damage'	")
            '" GROUP BY CustomerCode) as Ret      on vwcoadetail.coa_detail_id= ret.CustomerCode left outer join " & _
            '" (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.CurrentPrice) AS Dmg, CustomerCode, Sum(IsNull(SalesReturnDetailTable.Qty,0)) as UnsoldQty, SUM((SalesReturnDetailTable.CurrentPrice - SalesReturnDetailTable.Price) * SalesReturnDetailTable.Qty) as UnsoldMC " & _
            '" FROM         SalesReturnMasterTable INNER JOIN " & _
            '" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join " & _
            '" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            '" WHERE     ( convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            '" and isnull(tbldeflocation.location_type,'') <> 'Damage'	 ")
            '" GROUP BY CustomerCode) as Unsold  on vwcoadetail.coa_detail_id = Unsold.CustomerCode left outer join " & _
            '" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM         tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND " & _
            '" (tblVoucherDetail.credit_amount > 0) " & _
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid " & _
            '" left outer join tblcustomer on tblcustomer.AccountID = vwcoadetail.coa_detail_id")
            '" left outer join tbllistterritory on tbllistterritory.territoryID = tblcustomer.Territory" & _
            '" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblcustomer.customertypes  left outer join ")
            '" (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid" & _
            '" FROM    tblVoucher INNER JOIN " & _
            '" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            '" WHERE  (convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            '" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid WHERE vwcoadetail.Active=1 " & IIf(Me.rbtall.Checked = True, "", " AND vwcoadetail.account_type='Customer'") & "")
            'End Task:2409
            'Task:2459 Added Columns DispQty, RetQty, UnsoldQty,MiscAdj,Tax Amount 
            'and Price Apply By Retail Price And Invoice Price In This Query
            Dim strSQL As String
            'Before against task:2520
            '" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , Isnull(Dispatch.DispQty,0) as DispQty," & _
            '" isnull(ret.ret,0)as Ret,IsNull(Ret.ReturnQty,0)as RetQty, Case When isnull(ret.ret,0) > 0 Then (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) Else 0 End as [Ret%] ," & _
            '" isnull(Unsold.Dmg,0) as Unsold,Isnull(Unsold.UnSoldQty,0) as UnsoldQty,isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake], (IsNull(dispatch.SalesTax,0)-(Isnull(Ret.RetTax,0)+IsNull(Unsold.UnsoldTax,0))) as [Tax Amount]," & _
            '" isnull(PrevBal.pbal,0) + (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret, " & IIf(Me.rbtRetailPrice.Checked = True, "isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0))", "0") & " as MC, isnull(dispatch.Sampling,0) as Sampling, " & _
            '" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,  " & _
            '" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days], Isnull(MiscAdj,0) as MiscAdj")
            'Task:2520 Add AdjJv
            Dim FinishGoodsDepartment As String = getConfigValueByType("FinishGoodsDepartment").ToString

            strSQL = " select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , Isnull(Dispatch.DispQty,0) as DispQty," & _
            " isnull(ret.ret,0)as Ret,IsNull(Ret.ReturnQty,0)as RetQty, Case When isnull(ret.ret,0) > 0 Then (isnull(ret.ret,0) * 100)/ case when isnull(dispatch.dispatch, 1) = 0 then 1 else isnull(dispatch.dispatch, 1) end ELSE 0 End as [Ret%] ," & _
            " isnull(Unsold.Dmg,0) as Unsold,Isnull(Unsold.UnSoldQty,0) as UnsoldQty,isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake], (IsNull(dispatch.SalesTax,0)-(Isnull(Ret.RetTax,0)+IsNull(Unsold.UnsoldTax,0))) as [Tax Amount]," & _
            " isnull(PrevBal.pbal,0) + (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret, " & IIf(Me.rbtRetailPrice.Checked = True, "isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0))", "0") & " as MC, isnull(dispatch.Sampling,0) as Sampling, " & _
            " isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,  " & _
            " isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days], (Isnull(MiscAdj,0)+Isnull(AdjJv,0)) as MiscAdj" & _
            " from vwcoadetail  left outer join  " & _
            " (SELECT SUM(SalesDetailTable.dispatch) as dispatch, SUM(Isnull(SalesDetailTable.Qty,0)) as DispQty, CustomerCode, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, Count(Distinct SalesDate) as Days, Sum(Isnull(SalesDetailTable.SalesTax,0)) as SalesTax " & _
            " FROM  (SELECT  SalesId, SUM(Qty * " & IIf(Me.rbtRetailPrice.Checked = True, "CurrentPrice", "Price") & ") AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling, Sum(Isnull(SalesDetailTable.Qty,0)) as Qty, SUM(((Price*Qty)*TaxPercent)/100) as SalesTax  FROM dbo.SalesDetailTable WHERE SalesId In(Select SalesId From SalesMasterTable WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & "', 102))) GROUP BY SalesId) as SalesDetailTable INNER JOIN " & _
            " SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId " & IIf(FinishGoodsDepartment <> "", " INNER JOIN (SELECT SUM(SalesDetailTable.Qty) AS Qty, SalesDetailTable.SalesId FROM SalesDetailTable INNER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId WHERE (ArticleDefView.ArticleGroupId IN(" & FinishGoodsDepartment & ")) and (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & "', 102)) " & IIf(Me.chkUnpostedVoucher.Checked = True, "AND  isnull(SalesMasterTable.Post,0) in(1,0)", "AND isnull(SalesMasterTable.Post,0) in(1) ") & " group by CustomerCode, SalesDetailTable.SalesId) as DispQty ON SalesMasterTable.Salesid = DispQty.Salesid ", "") & "" & _
            " WHERE  " & IIf(Me.chkUnpostedVoucher.Checked = True, "  isnull(SalesMasterTable.Post,0) in(1,0) and ", "isnull(SalesMasterTable.Post,0) in(1) and ") & " (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))  " & _
            " GROUP BY CustomerCode ) as dispatch on vwcoadetail.coa_detail_id = dispatch.CustomerCode left outer join " & _
            " (SELECT     SUM(SalesReturnDetailTable.Qty * " & IIf(Me.rbtRetailPrice.Checked = True, "CurrentPrice", "Price") & ") AS Ret, CustomerCode, Sum(IsNull(SalesReturnDetailTable.Qty,0)) as ReturnQty, SUM((SalesReturnDetailTable.CurrentPrice - SalesReturnDetailTable.Price) * SalesReturnDetailTable.Qty) as ReturnMC, SUM(((Price*Qty)*Tax_Percent)/100) as RetTax " & _
            " FROM         SalesReturnMasterTable INNER JOIN " & _
            " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId   inner join " & _
            " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID " & IIf(FinishGoodsDepartment <> "", " INNER JOIN (SELECT SUM(SalesReturnDetailTable.Qty) AS ReturnQty , SalesReturnDetailTable.SalesReturnId FROM SalesReturnDetailTable INNER JOIN SalesReturnMasterTable ON SalesReturnDetailTable.SalesReturnId = SalesReturnMasterTable.SalesReturnId INNER JOIN ArticleDefView ON SalesReturnDetailTable.ArticleDefId = ArticleDefView.ArticleId WHERE (ArticleDefView.ArticleGroupId in (" & FinishGoodsDepartment & ")) and (convert(datetime, convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & "', 102)) " & IIf(Me.chkUnpostedVoucher.Checked = True, "AND  isnull(SalesReturnMasterTable.Post,0) in(1,0)", "AND isnull(SalesReturnMasterTable.Post,0) in(1) ") & " group by CustomerCode, SalesReturnDetailTable.SalesReturnId) as Ret on SalesReturnMasterTable.SalesReturnId = ret.SalesReturnId ", "") & "" & _
            " WHERE    " & IIf(Me.chkUnpostedVoucher.Checked = True, " isnull (SalesReturnMasterTable.Post,0) in (1,0) and ", "isnull(SalesReturnMasterTable.Post,0) in (1) and ") & " (convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            " and isnull(tbldeflocation.location_type,'') = 'Damage'" & _
            " GROUP BY CustomerCode) as Ret      on vwcoadetail.coa_detail_id= ret.CustomerCode left outer join " & _
            " (SELECT     SUM(SalesReturnDetailTable.Qty * " & IIf(Me.rbtRetailPrice.Checked = True, "CurrentPrice", "Price") & ") AS Dmg, CustomerCode, Sum(IsNull(SalesReturnDetailTable.Qty,0)) as UnsoldQty, SUM((SalesReturnDetailTable.CurrentPrice - SalesReturnDetailTable.Price) * SalesReturnDetailTable.Qty) as UnsoldMC,SUM(((Price*Qty)*Tax_Percent)/100) as UnSoldTax " & _
            " FROM         SalesReturnMasterTable INNER JOIN " & _
            " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join " & _
            " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  " & _
            " WHERE   " & IIf(Me.chkUnpostedVoucher.Checked = True, " isnull (SalesReturnMasterTable.Post,0) in (1,0) and ", "isnull(SalesReturnMasterTable.Post,0) in (1) and ") & "  (convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            " and isnull(tbldeflocation.location_type,'') <> 'Damage'	 " & _
            " GROUP BY CustomerCode) as Unsold  on vwcoadetail.coa_detail_id = Unsold.CustomerCode left outer join " & _
            " (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid" & _
            " FROM         tblVoucher INNER JOIN " & _
            " tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            " WHERE     voucher_type_id in (3,5)  and " & IIf(Me.chkUnpostedVoucher.Checked = True, "isnull(tblVoucher.Post ,0)in(1,0)  and", "isnull(tblvoucher.post,0) in (1) and ") & " (Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND " & _
            " (tblVoucherDetail.credit_amount > 0) " & _
            " GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid " & _
            " left outer join tblcustomer on tblcustomer.AccountID = vwcoadetail.coa_detail_id" & _
            " left outer join tbllistterritory on tbllistterritory.territoryID = tblcustomer.Territory" & _
            " left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblcustomer.customertypes  left outer join " & _
            " (SELECT     SUM(tblVoucherDetail.credit_amount) AS MiscAdj, tblVoucherDetail.coa_detail_id AS accid" & _
            " FROM         tblVoucher INNER JOIN " & _
            " tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            " WHERE     voucher_type_id in (2,4) and " & IIf(Me.chkUnpostedVoucher.Checked = True, "isnull(tblVoucher.Post ,0)in(1,0)  and", "isnull(tblvoucher.post,0) in (1) and ") & " (Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND " & _
            " (tblVoucherDetail.credit_amount > 0) " & _
            " GROUP BY tblVoucherDetail.coa_detail_id ) as cs1 on vwcoadetail.coa_detail_id= cs1.Accid  left outer join " & _
            " (SELECT     SUM(tblVoucherDetail.debit_amount-tblVoucherDetail.credit_amount) AS AdjJv, tblVoucherDetail.coa_detail_id AS accid" & _
            " FROM         tblVoucher INNER JOIN " & _
            " tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            " WHERE     voucher_type_id in (1,2,4,6) and " & IIf(Me.chkUnpostedVoucher.Checked = True, "isnull(tblVoucher.Post ,0)in(1,0)  and", "isnull(tblvoucher.post,0) in (1) and ") & " (Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))" & _
            " GROUP BY tblVoucherDetail.coa_detail_id ) as cs2 on vwcoadetail.coa_detail_id= cs2.Accid  left outer join " & _
            " (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid" & _
            " FROM    tblVoucher INNER JOIN " & _
            " tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " & _
            " WHERE " & IIf(Me.chkUnpostedVoucher.Checked = True, "isnull(tblVoucher.Post ,0)in(1,0)  and", "isnull(tblvoucher.post,0) in (1) and ") & "(convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) " & _
            " GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid WHERE vwcoadetail.Active=1 " & IIf(Me.rbtall.Checked = True, "", " AND vwcoadetail.account_type='Customer'") & ""
            'End Task:2459
            'End Task 2596
            Dim dt As DataTable = GetDataTable(strSQL)
            dt.Columns.Add("Net Sales", GetType(System.Double))  ' Task:2409 Added Net Sales Column
            dt.Columns.Add("ShortCash", GetType(System.Double))
            dt.Columns.Add("AddCash", GetType(System.Double)) ' Task:2409 Added Add Cash Column
            'dt.Columns.Add("SalesManAccess", GetType(System.Double))
            dt.Columns.Add("CurrBal", GetType(System.Double))
            'dt.Columns(EnumGrid.ShortCash).Expression = "((S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid])-[Offtake])"
            'Before against task:2459
            'dt.Columns(EnumGrid.ShortCash).Expression = "IIF ( ((S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid])-[Offtake]) < 0, ((S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid])-[Offtake]),0)"
            'Task:2459 Added Column Tax Amount And MiscAdj
            dt.Columns(EnumGrid.ShortCash).Expression = "IIF ( ((S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid])-([Offtake]+[Tax Amount])) < 0, ((S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid])-([Offtake]+[Tax Amount])),0)"
            'Before against task:2459
            'dt.Columns(EnumGrid.SaleManAccess).Expression = "(([Cash Paid])-([Offtake]-[Unsold]-MC))"
            'Comment against task:2531
            'dt.Columns(EnumGrid.CurrentBalance).Expression = "(Payable - (S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid]+[MiscAdj]))"
            'Task:2531 Misc Adjustmnet column's location change in Expression
            dt.Columns(EnumGrid.CurrentBalance).Expression = "(Payable - (S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid]))+[MiscAdj]+[Tax Amount]"
            'End Task:2531
            'End Task:2459
            '
            'Task:2409 Set Expression Net Sale and Add Cash Column
            'Before against task:2459
            'dt.Columns(EnumGrid.NetSales).Expression = "([OffTake]-(Petrol+Tooltax+Adjsmt+MC))"
            'TAsk:2459 Added Column Tax Amount 
            dt.Columns(EnumGrid.NetSales).Expression = "(([OffTake]+[Tax Amount])-(Petrol+Tooltax+Adjsmt+MC))"
            'End Task:2459
            dt.Columns(EnumGrid.AddCash).Expression = "IIF(ShortCash > 0, ShortCash, 0)"
            'End Task:2409
            'Me.grdRcords.RootTable.Columns("CurrBal").RightToLeft = Windows.Forms.RightToLeft.Yes
            'Task:2840 Hide Zero Value
            Dim dv As New DataView
            dt.TableName = "Default"
            dv.Table = dt
            If Me.chkHideZeroValue.Checked = True Then
                dv.RowFilter = "(Dispatch <> 0 Or  Ret <> 0 Or UnSold <> 0 Or Payable <> 0 Or CurrBal <> 0)"
            End If
            'Return dt
            Return dv.ToTable
            'End TaskL:2840
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub rbtall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtall.CheckedChanged, rbtcustomer.CheckedChanged
        Try
            If IsOpenForm = True Then btnGenerateReport_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub GetLayout()
    '    Try
    '        Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
    '        Me.grdRcords.LoadLayoutFile(fs1)
    '        fs1.Close()
    '        fs1.Dispose()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Daily Working Report"
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdRcords.LoadLayoutFile(fs1)
                fs1.Close()
                fs1.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Daily Working Report" & Chr(10) & "Date From: " & Me.dtpFrom.Value.ToString("dd-MM-yyyy").ToString & " Date To: " & Me.dtpTo.Value.ToString("dd-MM-yyyy").ToString & " "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
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
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rptDailyWorkingReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Me.cmbPeriod.Text = "Current Month"
            'Task No 2597 Fix The Check Box State True In This lINE oF cODE

            chkUnpostedVoucher.Checked = True
            'eND tASK
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptDailyWorking", , , , , , , GetData())
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''05-Mar-2014   TasK:2459  Imran Ali   Quantity Field on Daily Working Report
    Private Sub rbtRetailPrice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtRetailPrice.CheckedChanged
        Try
            If IsOpenForm = True Then btnGenerateReport_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rbtInvoicePrice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtInvoicePrice.CheckedChanged
        Try
            If IsOpenForm = True Then btnGenerateReport_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2459
    Private Sub rptDailyWorkingReport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.btnGenerateReport_Click(Nothing, Nothing)
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class