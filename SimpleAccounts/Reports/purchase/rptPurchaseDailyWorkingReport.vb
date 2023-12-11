''03-Jan-2014      Task:2409  Imran Ali           Net amount column should be added in sale report of daily working report.
''05-Mar-2014   TasK:2459  Imran Ali   Quantity Field on Daily Working Report
''27-Mar-2014 TASK:2520 Imran Ali   daily working report JV problem.
''1-Apr-2014 TASK:2535 Imran Ali Purchase Daily Working Division Error
''11-Apr-2014 TASK:2557 Imran Ali    purchase daily working problem
'Task No 2597 Mughees 28/4/2014 updating Query to view the unposted voucher 
''4-June-2014 Task:2670  Imran Ali Added TaxAmount in Current Balance
''12-Sep-2014 Task:2840 Imran Ali Zero Balance Hide In Purchase Daily Working Report
Public Class rptPurchaseDailyWorkingReport

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

            'Me.grdRcords.RootTable.Columns(EnumGrid.Type).Caption = "Customer Type"
            Me.grdRcords.RootTable.Columns(EnumGrid.Type).Caption = "Vendor Type"
            Me.grdRcords.RootTable.Columns(EnumGrid.ContDays).Caption = "Days"
            Me.grdRcords.RootTable.Columns(EnumGrid.NetSales).Caption = "Net Purchase"
            Me.grdRcords.RootTable.Columns(EnumGrid.Unsold).Caption = "Return"
            Me.grdRcords.RootTable.Columns(EnumGrid.UnsoldQty).Caption = "Return Qty"
            Me.grdRcords.RootTable.Columns(EnumGrid.Ret).Caption = "Damage"
            Me.grdRcords.RootTable.Columns(EnumGrid.RetQty).Caption = "Damage Qty"
            Me.grdRcords.RootTable.Columns(EnumGrid.Dispatch).Caption = "Purchase"
            Me.grdRcords.RootTable.Columns(EnumGrid.DispQty).Caption = "Purchase Qty"


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

            ''strSQL.AppendLine("     select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            ''strSQL.AppendLine(" isnull(ret.ret,0)as Ret ,   (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ,  ")
            ''strSQL.AppendLine(" isnull(Damage.Dmg,0) as Unsold , isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - isnull(ret.ret,0) as [Offtake],   ")
            ''strSQL.AppendLine(" isnull(PrevBal.pbal,0) + isnull(dispatch.dispatch,0) - isnull(ret.ret,0)  as Payable,  0 as S_ret,  isnull(dispatch.mc,0) as MC, isnull(dispatch.Sampling,0) as Sampling,   ")
            ''strSQL.AppendLine(" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,   ")
            ''strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid] ")
            ''strSQL.AppendLine(" from vwcoadetail  left outer join    ")
            ''strSQL.AppendLine(" (SELECT     SUM(ReceivingDetailTable.Qty * ReceivingDetailTable.Price) AS dispatch, VendorId, sum((ReceivingDetailTable.CurrentPrice - price)* ReceivingDetailTable.Qty) as MC,    ")
            ''strSQL.AppendLine(" sum(isnull(SampleQty,0)) as Sampling, isnull(Adjustment,0) as Adjsmt,   ")
            ''strSQL.AppendLine(" isnull(FuelExpense,0) as Petrol, isnull(otherExpense,0) as Tooltax, count(distinct ReceivingMasterTable.ReceivingDate) as Days   ")
            ''strSQL.AppendLine(" FROM         ReceivingDetailTable INNER JOIN   ")
            ''strSQL.AppendLine(" ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId   ")
            ''strSQL.AppendLine(" WHERE     (convert(datetime, convert(varchar, left(ReceivingMaterTable.ReceivingDate,11),102),102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "')   ")
            ''strSQL.AppendLine(" GROUP BY VendorId, isnull(Adjustment,0) ,isnull(FuelExpense,0) , isnull(otherExpense,0) ) as dispatch on vwcoadetail.coa_detail_id = dispatch.VendorId left outer join   ")
            ''strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.Price) AS Ret, VendorId   ")
            ''strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            ''strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId   inner join   ")
            ''strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            ''strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "')   ")
            ''strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') <> 'Damage'	")
            ''strSQL.AppendLine(" GROUP BY VendorId) as Ret      on vwcoadetail.coa_detail_id= ret.VendorId left outer join   ")
            ''strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.Price) AS Dmg, VendorId   ")
            ''strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            ''strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId inner join   ")
            ''strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            ''strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "')   ")
            ''strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') = 'Damage'	 ")
            ''strSQL.AppendLine(" GROUP BY VendorId) as Damage  on vwcoadetail.coa_detail_id = Damage.VendorId left outer join   ")
            ''strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid  ")
            ''strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            ''strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            ''strSQL.AppendLine(" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <='" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "' AND    ")
            ''strSQL.AppendLine(" (tblVoucherDetail.credit_amount > 0)   ")
            ''strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid   ")
            ''strSQL.AppendLine(" left outer join tblVendor on tblVendor.AccountID = vwcoadetail.coa_detail_id")
            ''strSQL.AppendLine(" left outer join tbllistterritory on tbllistterritory.territoryID = tblVendor.Territory  ")
            ''strSQL.AppendLine(" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblVendor.customertypes  left outer join ")
            ''strSQL.AppendLine(" (SELECT    sum(tblVoucherDetail.debit_amount) -   SUM(tblVoucherDetail.credit_amount) AS PBal, tblVoucherDetail.coa_detail_id AS accid  ")
            ''strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            ''strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            ''strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(tblVoucher.voucher_date,11),102),102)   < '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' ) AND  ")
            ''strSQL.AppendLine(" (tblVoucherDetail.credit_amount > 0)   ")
            ''strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid ")z
            'strSQL.AppendLine(" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            'strSQL.AppendLine(" isnull(ret.ret,0)as Ret ,   (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ,  ")
            'strSQL.AppendLine(" isnull(Damage.Dmg,0) as Unsold , isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - isnull(ret.ret,0) as [Offtake],   ")
            'strSQL.AppendLine(" isnull(PrevBal.pbal,0) + isnull(dispatch.dispatch,0) - isnull(ret.ret,0)  as Payable,  0 as S_ret,  isnull(dispatch.mc,0) as MC, isnull(dispatch.Sampling,0) as Sampling,   ")
            'strSQL.AppendLine(" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,   ")
            'strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days]")
            'strSQL.AppendLine(" from vwcoadetail  left outer join    ")
            'strSQL.AppendLine(" (SELECT      SUM(ReceivingDetailTable.dispatch) as dispatch, VendorId, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, COUNT(DISTINCT ReceivingMasterTable.ReceivingDate) AS Days ")
            'strSQL.AppendLine(" FROM        (SELECT     ReceivingId, SUM(Qty * Price) AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling FROM         dbo.ReceivingDetailTable GROUP BY ReceivingId) as ReceivingDetailTable INNER JOIN   ")
            'strSQL.AppendLine(" ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId   ")
            'strSQL.AppendLine(" WHERE     (convert(datetime, convert(varchar, left(ReceivingMaterTable.ReceivingDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" GROUP BY VendorId ) as dispatch on vwcoadetail.coa_detail_id = dispatch.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.Price) AS Ret, VendorId   ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId   inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') <> 'Damage'	")
            'strSQL.AppendLine(" GROUP BY VendorId) as Ret      on vwcoadetail.coa_detail_id= ret.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.Price) AS Dmg, VendorId   ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') = 'Damage'	 ")
            'strSQL.AppendLine(" GROUP BY VendorId) as Damage  on vwcoadetail.coa_detail_id = Damage.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND   ")
            'strSQL.AppendLine(" (tblVoucherDetail.credit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid   ")
            'strSQL.AppendLine(" left outer join tblVendor on tblVendor.AccountID = vwcoadetail.coa_detail_id")
            'strSQL.AppendLine(" left outer join tbllistterritory on tbllistterritory.territoryID = tblVendor.Territory  ")
            'strSQL.AppendLine(" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblVendor.customertypes  left outer join ")
            'strSQL.AppendLine(" (SELECT    sum(tblVoucherDetail.debit_amount) -   SUM(tblVoucherDetail.credit_amount) AS PBal, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(tblVoucher.voucher_date,11),102),102)   < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND  ")
            'strSQL.AppendLine(" (tblVoucherDetail.credit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid " & IIf(Me.rbtall.Checked = True, "", " where  vwcoadetail.account_type='Customer'") & "")

            'strSQL.AppendLine(" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            'strSQL.AppendLine(" isnull(ret.ret,0)as Ret,    (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ,  ")
            'strSQL.AppendLine(" isnull(Damage.Dmg,0) as Unsold, isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Damage.Dmg,0)) as [Offtake],   ")
            'strSQL.AppendLine(" isnull(PrevBal.pbal,0) + (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Damage.Dmg,0)))  as Payable,  0 as S_ret,  isnull(dispatch.mc,0) as MC, isnull(dispatch.Sampling,0) as Sampling,   ")
            'strSQL.AppendLine(" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,   ")
            'strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days]")
            'strSQL.AppendLine(" from vwcoadetail  left outer join    ")
            'strSQL.AppendLine(" (SELECT SUM(ReceivingDetailTable.dispatch) as dispatch, VendorId, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, COUNT(DISTINCT ReceivingMasterTable.ReceivingDate) AS Days ")
            'strSQL.AppendLine(" FROM  (SELECT     ReceivingId, SUM(Qty * CurrentPrice) AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling FROM         dbo.ReceivingDetailTable GROUP BY ReceivingId) as ReceivingDetailTable INNER JOIN   ")
            'strSQL.AppendLine(" ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId   ")
            'strSQL.AppendLine(" WHERE     (convert(datetime, convert(varchar, left(ReceivingMaterTable.ReceivingDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" GROUP BY VendorId ) as dispatch on vwcoadetail.coa_detail_id = dispatch.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.Price) AS Ret, VendorId   ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId   inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') <> 'Damage'	")
            'strSQL.AppendLine(" GROUP BY VendorId) as Ret      on vwcoadetail.coa_detail_id= ret.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.Price) AS Dmg, VendorId   ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') = 'Damage'	 ")
            'strSQL.AppendLine(" GROUP BY VendorId) as Damage  on vwcoadetail.coa_detail_id = Damage.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND   ")
            'strSQL.AppendLine(" (tblVoucherDetail.credit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid   ")
            'strSQL.AppendLine(" left outer join tblVendor on tblVendor.AccountID = vwcoadetail.coa_detail_id")
            'strSQL.AppendLine(" left outer join tbllistterritory on tbllistterritory.territoryID = tblVendor.Territory  ")
            'strSQL.AppendLine(" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblVendor.customertypes  left outer join ")
            'strSQL.AppendLine(" (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM    tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE  (convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid " & IIf(Me.rbtall.Checked = True, "", " where  vwcoadetail.account_type='Customer'") & "")



            'strSQL.AppendLine(" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            'strSQL.AppendLine(" isnull(ret.ret,0)as Ret,  (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ,  ")
            'strSQL.AppendLine(" isnull(Unsold.Dmg,0) as Unsold, isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake],   ")
            'strSQL.AppendLine(" isnull(PrevBal.pbal,0) + (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret,  isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0)) as MC, isnull(dispatch.Sampling,0) as Sampling,   ")
            'strSQL.AppendLine(" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,   ")
            'strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days]")
            'strSQL.AppendLine(" from vwcoadetail  left outer join    ")
            'strSQL.AppendLine(" (SELECT SUM(ReceivingDetailTable.dispatch) as dispatch, VendorId, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, Count(Distinct ReceivingDate) as Days ")
            'strSQL.AppendLine(" FROM  (SELECT  ReceivingId, SUM(Qty * CurrentPrice) AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling FROM dbo.ReceivingDetailTable GROUP BY ReceivingId) as ReceivingDetailTable INNER JOIN   ")
            'strSQL.AppendLine(" ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId ")
            'strSQL.AppendLine(" WHERE     (convert(datetime, convert(varchar, left(ReceivingMaterTable.ReceivingDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" GROUP BY VendorId ) as dispatch on vwcoadetail.coa_detail_id = dispatch.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.CurrentPrice) AS Ret, VendorId, Sum(IsNull(PurchaseReturnDetailTable.Qty,0)) as ReturnQty, SUM((PurchaseReturnDetailTable.CurrentPrice - PurchaseReturnDetailTable.Price) * PurchaseReturnDetailTable.Qty) as ReturnMC ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId   inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') = 'Damage'	")
            'strSQL.AppendLine(" GROUP BY VendorId) as Ret      on vwcoadetail.coa_detail_id= ret.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.CurrentPrice) AS Dmg, VendorId, Sum(IsNull(PurchaseReturnDetailTable.Qty,0)) as UnsoldQty, SUM((PurchaseReturnDetailTable.CurrentPrice - PurchaseReturnDetailTable.Price) * PurchaseReturnDetailTable.Qty) as UnsoldMC   ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') <> 'Damage'	 ")
            'strSQL.AppendLine(" GROUP BY VendorId) as Unsold  on vwcoadetail.coa_detail_id = Unsold.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND   ")
            'strSQL.AppendLine(" (tblVoucherDetail.credit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid   ")
            'strSQL.AppendLine(" left outer join tblVendor on tblVendor.AccountID = vwcoadetail.coa_detail_id")
            'strSQL.AppendLine(" left outer join tbllistterritory on tbllistterritory.territoryID = tblVendor.Territory  ")
            'strSQL.AppendLine(" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblVendor.customertypes  left outer join ")
            'strSQL.AppendLine(" (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM    tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE  (convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid WHERE vwcoadetail.Active=1 " & IIf(Me.rbtall.Checked = True, "", " AND vwcoadetail.account_type='Customer'") & "")




            'strSQL.AppendLine("     select CustomerName as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            'strSQL.AppendLine(" isnull(ret.ret,0)as Ret ,   (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ,  ")
            'strSQL.AppendLine(" isnull(Damage.Dmg,0) as Unsold , isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - isnull(ret.ret,0) as [Offtake],   ")
            'strSQL.AppendLine(" isnull(PrevBal.pbal,0) + isnull(dispatch.dispatch,0) - isnull(ret.ret,0)  as Payable,  0 as S_ret,  isnull(dispatch.mc,0) as MC, isnull(dispatch.Sampling,0) as Sampling,   ")
            'strSQL.AppendLine(" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,   ")
            'strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid] ")
            'strSQL.AppendLine(" from tblVendor  left outer join    ")
            'strSQL.AppendLine(" (SELECT     SUM(ReceivingDetailTable.Qty * ReceivingDetailTable.Price) AS dispatch, VendorId, sum((ReceivingDetailTable.CurrentPrice - price)* ReceivingDetailTable.Qty) as MC,    ")
            'strSQL.AppendLine(" sum(isnull(SampleQty,0)) as Sampling, sum(isnull(Adjustment,0)) as Adjsmt,   ")
            'strSQL.AppendLine(" sum(isnull(FuelExpense,0)) as Petrol, sum(isnull(otherExpense,0)) as Tooltax, count(ReceivingMasterTable.ReceivingId) as Days   ")
            'strSQL.AppendLine(" FROM         ReceivingDetailTable INNER JOIN   ")
            'strSQL.AppendLine(" ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId   ")
            'strSQL.AppendLine(" WHERE     (convert(datetime, convert(varchar, left(ReceivingMaterTable.ReceivingDate,11),102),102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "')   ")
            'strSQL.AppendLine(" GROUP BY VendorId) as dispatch on tblVendor.accountid = dispatch.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.Price) AS Ret, VendorId   ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId   ")
            'strSQL.AppendLine(" WHERE     ( convert(varchar, PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "')   ")
            'strSQL.AppendLine(" GROUP BY VendorId) as Ret      on tblVendor.accountid = ret.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.Price) AS Dmg, VendorId   ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnMasterTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(varchar, PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "')   ")
            'strSQL.AppendLine(" and tbldeflocation.location_type = 'Damage'	 ")
            'strSQL.AppendLine(" GROUP BY VendorId) as Damage  on tblVendor.accountid = Damage.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     (convert(varchar, tblVoucher.voucher_date,102)  BETWEEN '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' AND '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "') AND    ")
            'strSQL.AppendLine(" (tblVoucherDetail.credit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on tblVendor.accountid = cs.Accid   ")
            'strSQL.AppendLine(" left outer join tbllistterritory on tbllistterritory.territoryID = tblVendor.Territory  ")
            'strSQL.AppendLine(" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblVendor.customertypes  left outer join ")
            'strSQL.AppendLine("  (SELECT    sum(tblVoucherDetail.debit_amount) -   SUM(tblVoucherDetail.credit_amount) AS PBal, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     (convert(varchar, tblVoucher.voucher_date,102) < '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "' ) AND  ")
            'strSQL.AppendLine(" (tblVoucherDetail.credit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on tblVendor.accountid = PrevBal.accid ")

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
            'strSQL.AppendLine(" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            'strSQL.AppendLine(" isnull(ret.ret,0)as Ret,  (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) as [Ret%] ,  ")
            'strSQL.AppendLine(" isnull(Unsold.Dmg,0) as Unsold, isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake],   ")
            'strSQL.AppendLine(" isnull(PrevBal.pbal,0) + (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret,  isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0)) as MC, isnull(dispatch.Sampling,0) as Sampling,   ")
            'strSQL.AppendLine(" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,   ")
            'strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days]")
            'strSQL.AppendLine(" from vwcoadetail  left outer join    ")
            'strSQL.AppendLine(" (SELECT SUM(ReceivingDetailTable.dispatch) as dispatch, VendorId, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, Count(Distinct ReceivingDate) as Days ")
            'strSQL.AppendLine(" FROM  (SELECT  ReceivingId, SUM(Qty * CurrentPrice) AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling FROM dbo.ReceivingDetailTable GROUP BY ReceivingId) as ReceivingDetailTable INNER JOIN   ")
            'strSQL.AppendLine(" ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId ")
            'strSQL.AppendLine(" WHERE     (convert(datetime, convert(varchar, left(ReceivingMaterTable.ReceivingDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" GROUP BY VendorId ) as dispatch on vwcoadetail.coa_detail_id = dispatch.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.CurrentPrice) AS Ret, VendorId, Sum(IsNull(PurchaseReturnDetailTable.Qty,0)) as ReturnQty, SUM((PurchaseReturnDetailTable.CurrentPrice - PurchaseReturnDetailTable.Price) * PurchaseReturnDetailTable.Qty) as ReturnMC ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId   inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') = 'Damage'	")
            'strSQL.AppendLine(" GROUP BY VendorId) as Ret      on vwcoadetail.coa_detail_id= ret.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.CurrentPrice) AS Dmg, VendorId, Sum(IsNull(PurchaseReturnDetailTable.Qty,0)) as UnsoldQty, SUM((PurchaseReturnDetailTable.CurrentPrice - PurchaseReturnDetailTable.Price) * PurchaseReturnDetailTable.Qty) as UnsoldMC   ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') <> 'Damage'	 ")
            'strSQL.AppendLine(" GROUP BY VendorId) as Unsold  on vwcoadetail.coa_detail_id = Unsold.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND   ")
            'strSQL.AppendLine(" (tblVoucherDetail.credit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid   ")
            'strSQL.AppendLine(" left outer join tblVendor on tblVendor.AccountID = vwcoadetail.coa_detail_id")
            'strSQL.AppendLine(" left outer join tbllistterritory on tbllistterritory.territoryID = tblVendor.Territory  ")
            'strSQL.AppendLine(" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblVendor.customertypes  left outer join ")
            'strSQL.AppendLine(" (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM    tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE  (convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid WHERE vwcoadetail.Active=1 " & IIf(Me.rbtall.Checked = True, "", " AND vwcoadetail.account_type='Customer'") & "")
            'Before against task:2459
            'Task:2409 Zero Return Column Calc Problem
            'Dim strSQL As New System.Text.StringBuilder
            'strSQL.AppendLine(" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tbldefcustomertype.name as type,   isnull(dispatch.dispatch,0) as Dispatch  , ")
            'strSQL.AppendLine(" isnull(ret.ret,0)as Ret,  Case When isnull(ret.ret,0) > 0 Then (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) Else 0 End as [Ret%] ,  ")
            'strSQL.AppendLine(" isnull(Unsold.Dmg,0) as Unsold, isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake],   ")
            'strSQL.AppendLine(" isnull(PrevBal.pbal,0) + (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret,  isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0)) as MC, isnull(dispatch.Sampling,0) as Sampling,   ")
            'strSQL.AppendLine(" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,   ")
            'strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days]")
            'strSQL.AppendLine(" from vwcoadetail  left outer join    ")
            'strSQL.AppendLine(" (SELECT SUM(ReceivingDetailTable.dispatch) as dispatch, VendorId, SUM(MC) AS MC, SUM(Sampling) AS Sampling, sum(isnull(Adjustment, 0)) AS Adjsmt, sum(isnull(FuelExpense, 0)) AS Petrol, sum(isnull(otherExpense, 0)) AS Tooltax, Count(Distinct ReceivingDate) as Days ")
            'strSQL.AppendLine(" FROM  (SELECT  ReceivingId, SUM(Qty * CurrentPrice) AS dispatch, SUM((CurrentPrice - Price) * Qty) AS MC, SUM(ISNULL(SampleQty, 0)) AS Sampling FROM dbo.ReceivingDetailTable GROUP BY ReceivingId) as ReceivingDetailTable INNER JOIN   ")
            'strSQL.AppendLine(" ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId ")
            'strSQL.AppendLine(" WHERE     (convert(datetime, convert(varchar, left(ReceivingMaterTable.ReceivingDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" GROUP BY VendorId ) as dispatch on vwcoadetail.coa_detail_id = dispatch.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.CurrentPrice) AS Ret, VendorId, Sum(IsNull(PurchaseReturnDetailTable.Qty,0)) as ReturnQty, SUM((PurchaseReturnDetailTable.CurrentPrice - PurchaseReturnDetailTable.Price) * PurchaseReturnDetailTable.Qty) as ReturnMC ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId   inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') = 'Damage'	")
            'strSQL.AppendLine(" GROUP BY VendorId) as Ret      on vwcoadetail.coa_detail_id= ret.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.CurrentPrice) AS Dmg, VendorId, Sum(IsNull(PurchaseReturnDetailTable.Qty,0)) as UnsoldQty, SUM((PurchaseReturnDetailTable.CurrentPrice - PurchaseReturnDetailTable.Price) * PurchaseReturnDetailTable.Qty) as UnsoldMC   ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') <> 'Damage'	 ")
            'strSQL.AppendLine(" GROUP BY VendorId) as Unsold  on vwcoadetail.coa_detail_id = Unsold.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND   ")
            'strSQL.AppendLine(" (tblVoucherDetail.credit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid   ")
            'strSQL.AppendLine(" left outer join tblVendor on tblVendor.AccountID = vwcoadetail.coa_detail_id")
            'strSQL.AppendLine(" left outer join tbllistterritory on tbllistterritory.territoryID = tblVendor.Territory  ")
            'strSQL.AppendLine(" left outer join tbldefCustomerType on tbldefCustomerType.TypeID = tblVendor.customertypes  left outer join ")
            'strSQL.AppendLine(" (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM    tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE  (convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid WHERE vwcoadetail.Active=1 " & IIf(Me.rbtall.Checked = True, "", " AND vwcoadetail.account_type='Customer'") & "")
            'End Task:2409
            'Task:2459 Added Columns DispQty, RetQty, UnsoldQty,MiscAdj,Tax Amount 
            'and Price Apply By Retail Price And Invoice Price In This Query
            Dim strSQL As New System.Text.StringBuilder
            'Before against task:2520
            'strSQL.AppendLine(" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, '' as type,   isnull(dispatch.dispatch,0) as Dispatch  , Isnull(Dispatch.DispQty,0) as DispQty,  ")
            'strSQL.AppendLine(" isnull(ret.ret,0)as Ret,IsNull(Ret.ReturnQty,0)as RetQty, Case When isnull(ret.ret,0) > 0 Then (isnull(ret.ret,0) * 100)/ isnull(dispatch.dispatch,1) Else 0 End as [Ret%] ,  ")
            'strSQL.AppendLine(" isnull(Unsold.Dmg,0) as Unsold,Isnull(Unsold.UnSoldQty,0) as UnsoldQty,isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake], (IsNull(dispatch.SalesTax,0)-(Isnull(Ret.RetTax,0)+IsNull(Unsold.UnsoldTax,0))) as [Tax Amount],  ")
            'strSQL.AppendLine(" isnull(PrevBal.pbal,0) - (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret, " & IIf(Me.rbtRetailPrice.Checked = True, "isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0))", "0") & " as MC, isnull(dispatch.Sampling,0) as Sampling,   ")
            'strSQL.AppendLine(" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,    ")
            'strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days], (Isnull(MiscAdj,0)+IsNull(AdjJv,0)) as MiscAdj")
            'Task:2520 Add JV Voucher Adjustment
            'strSQL.AppendLine(" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, '' as type,   isnull(dispatch.dispatch,0) as Dispatch  , Isnull(Dispatch.DispQty,0) as DispQty,  ")
            'strSQL.AppendLine(" isnull(ret.ret,0)as Ret,IsNull(Ret.ReturnQty,0)as RetQty, 0 as [Ret%] ,  ")
            'strSQL.AppendLine(" isnull(Unsold.Dmg,0) as Unsold,Isnull(Unsold.UnSoldQty,0) as UnsoldQty,isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake], (IsNull(dispatch.SalesTax,0)-(Isnull(Ret.RetTax,0)+IsNull(Unsold.UnsoldTax,0))) as [Tax Amount],  ")
            'strSQL.AppendLine(" isnull(PrevBal.pbal,0) - (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret, " & IIf(Me.rbtRetailPrice.Checked = True, "isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0))", "0") & " as MC, isnull(dispatch.Sampling,0) as Sampling,   ")
            'strSQL.AppendLine(" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,    ")
            'strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days], (Isnull(MiscAdj,0)+IsNull(AdjJv,0)) as MiscAdj")
            ''End Task:2520
            'strSQL.AppendLine(" from vwcoadetail  left outer join    ")
            'strSQL.AppendLine(" (SELECT SUM(ReceivingDetailTable.dispatch) as dispatch, SUM(Isnull(ReceivingDetailTable.Qty,0)) as DispQty, VendorId, SUM(MC) AS MC,0 AS Sampling, 0 AS Adjsmt, 0 AS Petrol, 0 AS Tooltax, Count(Distinct ReceivingDate) as Days, Sum(Isnull(ReceivingDetailTable.SalesTax,0)) as SalesTax ")
            'strSQL.AppendLine(" FROM  (SELECT  ReceivingId, SUM(Qty * Price)  AS dispatch, " & IIf(Me.rbtRetailPrice.Checked = True, " SUM((CurrentPrice - Price) * Qty)", "0") & " AS MC, 0 AS Sampling, Sum(Isnull(ReceivingDetailTable.Qty,0)) as Qty, SUM(((Price*Qty)*TaxPercent)/100) as SalesTax  FROM dbo.ReceivingDetailTable GROUP BY ReceivingId) as ReceivingDetailTable INNER JOIN ")
            'strSQL.AppendLine(" ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId ")
            'strSQL.AppendLine(" WHERE     (convert(datetime, convert(varchar, left(ReceivingMasterTable.ReceivingDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & "', 102))  ")
            'strSQL.AppendLine(" GROUP BY VendorId  ) as dispatch on vwcoadetail.coa_detail_id = dispatch.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * " & IIf(Me.rbtRetailPrice.Checked = True, "CurrentPrice", "Price") & ") AS Ret, VendorId, Sum(IsNull(PurchaseReturnDetailTable.Qty,0)) as ReturnQty, SUM((PurchaseReturnDetailTable.CurrentPrice - PurchaseReturnDetailTable.Price) * PurchaseReturnDetailTable.Qty) as ReturnMC, SUM(((Price*Qty)*Tax_Percent)/100) as RetTax ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId   inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') = 'Damage'	")
            'strSQL.AppendLine(" GROUP BY VendorId) as Ret      on vwcoadetail.coa_detail_id= ret.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * " & IIf(Me.rbtRetailPrice.Checked = True, "CurrentPrice", "Price") & ") AS Dmg, VendorId, Sum(IsNull(PurchaseReturnDetailTable.Qty,0)) as UnsoldQty, SUM((PurchaseReturnDetailTable.CurrentPrice - PurchaseReturnDetailTable.Price) * PurchaseReturnDetailTable.Qty) as UnsoldMC,SUM(((Price*Qty)*Tax_Percent)/100) as UnSoldTax   ")
            'strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            'strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId inner join   ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            'strSQL.AppendLine(" WHERE     ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            'strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') <> 'Damage'	 ")
            'strSQL.AppendLine(" GROUP BY VendorId) as Unsold  on vwcoadetail.coa_detail_id = Unsold.VendorId left outer join   ")
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.debit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     voucher_type_id in (2,4)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND   ")
            'strSQL.AppendLine(" (tblVoucherDetail.debit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid   ")
            'strSQL.AppendLine(" left outer join tblVendor on tblVendor.AccountID = vwcoadetail.coa_detail_id")
            'strSQL.AppendLine(" left outer join tbllistterritory on tbllistterritory.territoryID = tblVendor.Territory  ")
            'strSQL.AppendLine(" left outer join   ")
            ''Before against task:2492
            ''strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.debit_amount) AS MiscAdj, tblVoucherDetail.coa_detail_id AS accid  ")
            ''strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            ''strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            ''strSQL.AppendLine(" WHERE     voucher_type_id in (1)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) ") 'AND   ")
            ''strSQL.AppendLine(" AND (tblVoucherDetail.debit_amount > 0)   ")
            ''Task:2492 Added Voucher Type Id 3,5 in this query
            ''Before against task:2520
            ''strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.debit_amount) AS MiscAdj, tblVoucherDetail.coa_detail_id AS accid  ")
            ''strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            ''strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            ''strSQL.AppendLine(" WHERE     voucher_type_id in (1,3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) ") 'AND   ")
            ''strSQL.AppendLine(" AND (tblVoucherDetail.debit_amount > 0)   ")
            ''End Task:2492
            ''Task:2520 Remove JV Voucher Type Id 1
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.debit_amount) AS MiscAdj, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     voucher_type_id in (3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) ") 'AND   ")
            'strSQL.AppendLine(" AND (tblVoucherDetail.debit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs1 on vwcoadetail.coa_detail_id= cs1.Accid   ")
            ''End Task:2520
            'strSQL.AppendLine(" left outer join ")
            ''Task:2520 Add Jv Voucher
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.debit_amount-credit_amount) AS AdjJv, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     voucher_type_id in (1)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) ") 'AND   ")
            ''strSQL.AppendLine(" AND (tblVoucherDetail.debit_amount > 0)   ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs2 on vwcoadetail.coa_detail_id= cs2.Accid   ")
            ''End Task:2520
            'strSQL.AppendLine(" left outer join ")
            'strSQL.AppendLine(" (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM    tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE  (convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            'strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid WHERE vwcoadetail.Active=1 " & IIf(Me.rbtall.Checked = True, "", " AND vwcoadetail.account_type='Vendor'") & "")
            'End Task:2459

            'Task No 2597 Update The Query For Purchase For UnPosted Vouchers
            'Ali Faisal : TFS2277 : Add Additional sales tax in Tax Amount
            strSQL.AppendLine(" select detail_title as [Name],tbllistTerritory.TerritoryName as Sector, tblVendorType.VendorType as type,   isnull(dispatch.dispatch,0) as Dispatch  , Isnull(Dispatch.DispQty,0) as DispQty,  ")
            strSQL.AppendLine(" isnull(ret.ret,0)as Ret,IsNull(Ret.ReturnQty,0)as RetQty, 0 as [Ret%] ,  ")
            strSQL.AppendLine(" isnull(Unsold.Dmg,0) as Unsold,Isnull(Unsold.UnSoldQty,0) as UnsoldQty,isnull(PrevBal.pbal,0) as PrevBal, isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)) as [Offtake], (IsNull(dispatch.SalesTax,0)+IsNull(dispatch.AdSalesTax,0)-(Isnull(Ret.RetTax,0)+IsNull(Unsold.UnsoldTax,0))) as [Tax Amount],  ")
            strSQL.AppendLine(" isnull(PrevBal.pbal,0) - (isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Unsold.Dmg,0)))  as Payable,  0 as S_ret, " & IIf(Me.rbtRetailPrice.Checked = True, "isnull(dispatch.MC,0)-(IsNull(Ret.ReturnMC,0)+IsNull(Unsold.UnsoldMC,0))", "0") & " as MC, isnull(dispatch.Sampling,0) as Sampling,   ")
            strSQL.AppendLine(" isnull(dispatch.Petrol,0) as Petrol,isnull(dispatch.Tooltax,0) as Tooltax,  isnull(dispatch.Adjsmt,0) as Adjsmt,    ")
            strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid], IsNull(dispatch.Days,0) as [# Of Days], (Isnull(MiscAdj,0)+IsNull(AdjJv,0)) as MiscAdj")
            'End Task:2520
            strSQL.AppendLine(" from vwcoadetail  left outer join    ")
            strSQL.AppendLine(" (SELECT SUM(ReceivingDetailTable.dispatch) as dispatch, SUM(Isnull(ReceivingDetailTable.Qty,0)) as DispQty, VendorId, SUM(MC) AS MC,0 AS Sampling, 0 AS Adjsmt, 0 AS Petrol, 0 AS Tooltax, Count(Distinct ReceivingDate) as Days, Sum(Isnull(ReceivingDetailTable.SalesTax,0)) as SalesTax, SUM(ISNULL(ReceivingDetailTable.AdSalesTax,0))AS AdSalesTax ")
            strSQL.AppendLine(" FROM  (SELECT  ReceivingId, SUM(Qty * Price)  AS dispatch, " & IIf(Me.rbtRetailPrice.Checked = True, " SUM((CurrentPrice - Price) * Qty)", "0") & " AS MC, 0 AS Sampling, Sum(Isnull(ReceivingDetailTable.Qty,0)) as Qty, SUM(((Price*Qty)*TaxPercent)/100) as SalesTax, SUM(Price * Qty * AdTax_Percent / 100) AS AdSalesTax  FROM dbo.ReceivingDetailTable GROUP BY ReceivingId) as ReceivingDetailTable INNER JOIN ")
            'Ali Faisal : TFS2277 : End
            strSQL.AppendLine(" ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId ")
            strSQL.AppendLine(" WHERE " & IIf(Me.chkUnPostedVoucher.Checked = True, "  isnull(ReceivingMasterTable.Post,0) in(1,0) and ", "isnull(ReceivingMasterTable.Post,0) in(1) and ") & " (convert(datetime, convert(varchar, left(ReceivingMasterTable.ReceivingDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & "', 102))  ")
            strSQL.AppendLine(" GROUP BY VendorId  ) as dispatch on vwcoadetail.coa_detail_id = dispatch.VendorId left outer join   ")
            strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * " & IIf(Me.rbtRetailPrice.Checked = True, "CurrentPrice", "Price") & ") AS Ret, VendorId, Sum(IsNull(PurchaseReturnDetailTable.Qty,0)) as ReturnQty, SUM((PurchaseReturnDetailTable.CurrentPrice - PurchaseReturnDetailTable.Price) * PurchaseReturnDetailTable.Qty) as ReturnMC, SUM(((Price*Qty)*Tax_Percent)/100) as RetTax ")
            strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId   inner join   ")
            strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            strSQL.AppendLine(" WHERE  " & IIf(Me.chkUnPostedVoucher.Checked = True, "  isnull(PurchaseReturnMasterTable.Post,0) in(1,0) and ", "isnull(PurchaseReturnMasterTable.Post,0) in(1) and ") & " ( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') = 'Damage'	")
            strSQL.AppendLine(" GROUP BY VendorId) as Ret      on vwcoadetail.coa_detail_id= ret.VendorId left outer join   ")
            strSQL.AppendLine(" (SELECT     SUM(PurchaseReturnDetailTable.Qty * " & IIf(Me.rbtRetailPrice.Checked = True, "CurrentPrice", "Price") & ") AS Dmg, VendorId, Sum(IsNull(PurchaseReturnDetailTable.Qty,0)) as UnsoldQty, SUM((PurchaseReturnDetailTable.CurrentPrice - PurchaseReturnDetailTable.Price) * PurchaseReturnDetailTable.Qty) as UnsoldMC,SUM(((Price*Qty)*Tax_Percent)/100) as UnSoldTax   ")
            strSQL.AppendLine(" FROM         PurchaseReturnMasterTable INNER JOIN   ")
            strSQL.AppendLine(" PurchaseReturnDetailTable ON PurchaseReturnMasterTable.PurchaseReturnId = PurchaseReturnDetailTable.PurchaseReturnId inner join   ")
            strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = PurchaseReturnDetailTable.locationID    ")
            strSQL.AppendLine(" WHERE " & IIf(Me.chkUnPostedVoucher.Checked = True, "  isnull(PurchaseReturnMasterTable.Post,0) in(1,0) and ", "isnull(PurchaseReturnMasterTable.Post,0) in(1) and ") & "( convert(datetime , convert(varchar, left(PurchaseReturnMasterTable.PurchaseReturnDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))   ")
            strSQL.AppendLine(" and isnull(tbldeflocation.location_type,'') <> 'Damage'	 ")
            strSQL.AppendLine(" GROUP BY VendorId) as Unsold  on vwcoadetail.coa_detail_id = Unsold.VendorId left outer join   ")
            strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.debit_amount-tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid  ")
            strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            strSQL.AppendLine(" WHERE " & IIf(Me.chkUnPostedVoucher.Checked = True, "isnull(tblVoucher.Post ,0)in(1,0)  and", "isnull(tblvoucher.post,0) in (1) and ") & " voucher_type_id in (2,4)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)   ")
            ' strSQL.AppendLine(" (tblVoucherDetail.debit_amount > 0)   ")
            strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on vwcoadetail.coa_detail_id= cs.Accid   ")
            strSQL.AppendLine(" left outer join tblVendor on tblVendor.AccountID = vwcoadetail.coa_detail_id")
            strSQL.AppendLine(" left outer join tbllistterritory on tbllistterritory.territoryID = tblVendor.Territory  ")
            ''4-June-2014 Task:2670  Imran Ali Added TaxAmount in Current Balance
            strSQL.AppendLine(" left outer join tblVendorType on tblVendorType.VendorTypeId = tblVendor.VendorsTypeId ")
            'End Task:2670
            strSQL.AppendLine(" left outer join   ")
            'Before against task:2492
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.debit_amount) AS MiscAdj, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     voucher_type_id in (1)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) ") 'AND   ")
            'strSQL.AppendLine(" AND (tblVoucherDetail.debit_amount > 0)   ")
            'Task:2492 Added Voucher Type Id 3,5 in this query
            'Before against task:2520
            'strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.debit_amount) AS MiscAdj, tblVoucherDetail.coa_detail_id AS accid  ")
            'strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            'strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            'strSQL.AppendLine(" WHERE     voucher_type_id in (1,3,5)  and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) ") 'AND   ")
            'strSQL.AppendLine(" AND (tblVoucherDetail.debit_amount > 0)   ")
            'End Task:2492
            'Task:2520 Remove JV Voucher Type Id 1
            strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.debit_amount) AS MiscAdj, tblVoucherDetail.coa_detail_id AS accid  ")
            strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            strSQL.AppendLine(" WHERE     (tblVoucher.voucher_code like '%Pur%' )  and " & IIf(Me.chkUnPostedVoucher.Checked = True, "isnull(tblVoucher.Post ,0)in(1,0)  and", "isnull(tblvoucher.post,0) in (1) and ") & " ( Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            ' strSQL.AppendLine(" WHERE     (voucher_type_id in (3,5) OR tblVoucher.voucher_code like '%Pur%' )  and " & IIf(Me.chkUnPostedVoucher.Checked = True, "isnull(tblVoucher.Post ,0)in(1,0)  and", "isnull(tblvoucher.post,0) in (1) and ") & " ( Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs1 on vwcoadetail.coa_detail_id= cs1.Accid   ")
            'End Task:2520
            strSQL.AppendLine(" left outer join ")
            'Task:2520 Add Jv Voucher
            strSQL.AppendLine(" (SELECT     SUM(tblVoucherDetail.debit_amount-credit_amount) AS AdjJv, tblVoucherDetail.coa_detail_id AS accid  ")
            strSQL.AppendLine(" FROM         tblVoucher INNER JOIN   ")
            strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            strSQL.AppendLine(" WHERE     voucher_type_id in (1,3,5,7)  and " & IIf(Me.chkUnPostedVoucher.Checked = True, "isnull(tblVoucher.Post ,0)in(1,0)  and", "isnull(tblvoucher.post,0) in (1) and ") & " ( Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and Convert(datetime,convert(varchar,tblVoucher.voucher_date,102)) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))")
            'strSQL.AppendLine(" AND (tblVoucherDetail.debit_amount > 0)   ")
            strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs2 on vwcoadetail.coa_detail_id= cs2.Accid   ")
            'End Task:2520
            strSQL.AppendLine(" left outer join ")
            strSQL.AppendLine(" (SELECT    SUM((tblVoucherDetail.debit_amount) - (tblVoucherDetail.credit_amount)) AS PBal, tblVoucherDetail.coa_detail_id AS accid  ")
            strSQL.AppendLine(" FROM    tblVoucher INNER JOIN   ")
            strSQL.AppendLine(" tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id   ")
            strSQL.AppendLine(" WHERE " & IIf(Me.chkUnPostedVoucher.Checked = True, "isnull(tblVoucher.Post ,0)in(1,0)  and", "isnull(tblvoucher.post,0) in (1) and ") & "(convert(datetime,convert(varchar,left(tblVoucher.voucher_date,11),102),102) < Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as PrevBal on vwcoadetail.coa_detail_id = PrevBal.accid WHERE vwcoadetail.Active=1 " & IIf(Me.rbtall.Checked = True, "", " AND vwcoadetail.account_type='Vendor'") & "")

            'End Task 2597
            Dim dt As DataTable = GetDataTable(strSQL.ToString())
            'Dim str As String = strSQL.ToString

            dt.Columns.Add("Net Sales", GetType(System.Double))  ' Task:2409 Added Net Sales Column
            dt.Columns.Add("ShortCash", GetType(System.Double))
            dt.Columns.Add("AddCash", GetType(System.Double)) ' Task:2409 Added Add Cash Column
            'dt.Columns.Add("SalesManAccess", GetType(System.Double))
            dt.Columns.Add("CurrBal", GetType(System.Double))
            'dt.Columns(EnumGrid.ShortCash).Expression = "((S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid])-[Offtake])"
            'Before against task:2459
            'dt.Columns(EnumGrid.ShortCash).Expression = "IIF ( ((S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid])-[Offtake]) < 0, ((S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid])-[Offtake]),0)"
            'Task:2459 Added Column Tax Amount And MiscAdj
            'dt.Columns(EnumGrid.ShortCash).Expression = "IIF([Cash Paid]-[Net Sales] < 0, [Cash Paid]-[Net Sales],0)" ''"IIF ([Cash Paid]- ((S_ret + MC + Petrol + Tooltax + Adjsmt)+([Offtake]+[Tax Amount])) > 0, ([Cash Paid]-(S_ret + MC + Petrol + Tooltax + Adjsmt)+([Offtake]+[Tax Amount])),0)"
            dt.Columns(EnumGrid.ShortCash).Expression = "IIF(([Cash Paid]-[Net Sales]) < 0, ([Cash Paid]-[Net Sales]),0)" ''"IIF ([Cash Paid]- ((S_ret + MC + Petrol + Tooltax + Adjsmt)+([Offtake]+[Tax Amount])) > 0, ([Cash Paid]-(S_ret + MC + Petrol + Tooltax + Adjsmt)+([Offtake]+[Tax Amount])),0)"
            'Before against task:2459
            'dt.Columns(EnumGrid.SaleManAccess).Expression = "(([Cash Paid])-([Offtake]-[Unsold]-MC))"
            'Before against task:2557
            'dt.Columns(EnumGrid.CurrentBalance).Expression = "(Payable + (S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid]))-[MiscAdj]"
            'Task:2557  MiscAdj Calc Changed
            dt.Columns(EnumGrid.CurrentBalance).Expression = "(Payable + (S_ret + MC + Petrol + Tooltax + Adjsmt + [Cash Paid]))+[MiscAdj]-[Tax Amount]"
            'end Task:2557
            'End Task:2459
            '
            'Task:2409 Set Expression Net Sale and Add Cash Column
            'Before against task:2459
            'dt.Columns(EnumGrid.NetSales).Expression = "([OffTake]-(Petrol+Tooltax+Adjsmt+MC))"
            'TAsk:2459 Added Column Tax Amount 
            dt.Columns(EnumGrid.NetSales).Expression = "(([OffTake]+[Tax Amount])+(Petrol+Tooltax+Adjsmt+MC))"
            'End Task:2459
            'dt.Columns(EnumGrid.AddCash).Expression = "IIF([Cash Paid] - [Net Sales] > 0, [Cash Paid] - [Net Sales], 0)"
            dt.Columns(EnumGrid.AddCash).Expression = "IIF(([Cash Paid] - [Net Sales]) > 0, ([Cash Paid] - [Net Sales]), 0)"
            'End Task:2409
            'Me.grdRcords.RootTable.Columns("CurrBal").RightToLeft = Windows.Forms.RightToLeft.Yes
            'ask:2840 Filter Zero Value
            Dim dv As New DataView
            dt.TableName = "Default"
            dv.Table = dt
            If Me.chkHideZeroValue.Checked = True Then
                dv.RowFilter = "(Dispatch <> 0 Or  Ret <> 0 Or UnSold <> 0 Or Payable <> 0 Or CurrBal <> 0)"
            End If
            'Return dt
            Return dv.ToTable
            'End Task:2840
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

    Private Sub rptPurchaseDailyWorkingReport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            btnGenerateReport_Click(Nothing, Nothing)
            IsOpenForm = True
        Catch ex As Exception

        End Try
    End Sub
End Class