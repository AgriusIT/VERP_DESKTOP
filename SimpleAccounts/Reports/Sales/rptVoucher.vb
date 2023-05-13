''10-12-2015 TASKTFS129 Muhammad Ameen :S-55 Customer Dealer Voucher: Remove inactive customer from the customers list
Public Class rptVoucher
    Implements IGeneral
    Dim flgPrintLog As Boolean = False
    Dim PrintCont As Int32 = 0I
    Dim intPrintCont As Integer = 0I
    Dim UserGroup As String
    Private Sub rptVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If Not GetConfigValue("PrintLog").ToString = "Error" Then
                flgPrintLog = Convert.ToBoolean(GetConfigValue("PrintLog"))
            Else
                flgPrintLog = False
            End If

            If Not GetConfigValue("PrintCount").ToString = "Error" Then
                PrintCont = Convert.ToInt32(GetConfigValue("PrintCount"))
            Else
                PrintCont = 0
            End If

            UserGroup = getUserGroup(LoginUserId)
            Me.FillCombos()
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
        Try
            FillDropDown(Me.cmbSalesman, "select coa_detail_id,detail_title from vwcoadetail where account_type='Customer' And Active <> 0 order by 2", True) ''TASKTFS129
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbSalesman.SelectedIndex = 0 Then
                ShowValidationMessage("Please Select Salesman/Dealder")
                Me.cmbSalesman.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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
    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try

            'If flgPrintLog = True Then
            '    Dim PrintLog As New SBModel.PrintVoucherLogBE
            '    PrintLog.SaleManId = Me.cmbSalesman.SelectedValue
            '    PrintLog.VoucherDate = Me.dtpFrom.Value
            '    PrintLog.UserName = LoginUserName
            '    Call SBDal.PrintVoucherLogDAL.Save(PrintLog)

            '    If Not getUserGroup(LoginUserId) = "Administrator" Then
            '        If PrintCont > getPrintCont(Me.cmbSalesman.SelectedValue, Me.dtpFrom.Value) Then
            '            intPrintCont = getPrintCont(Me.cmbSalesman.SelectedValue, Me.dtpFrom.Value)
            '        Else
            '            ShowErrorMessage("Print not allowed")
            '            Exit Sub
            '        End If
            '    Else
            '        intPrintCont = getPrintCont(Me.cmbSalesman.SelectedValue, Me.dtpFrom.Value)
            '    End If
            'End If
            ShowReport("rptSalesmandealerVoucher", , , , False, , , Me.MakeDataTable(), , rptReportViewerNew.enmReport.SalesmanVoucher)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Function MakeDataTable() As DataTable
        Try
            Dim dtMain As DataTable
            ', "'" & Me.cmbSalesman.Text & "'"
            'dtMain.Columns.Add("SerialNo", GetType(System.String))
            'dtMain.Columns.Add("SerialNo", GetType(System.String))

            Dim strSQL As String = ""

            'strSQL = " Select Sales.[Serial No] AS [SerialNo],  Sales.SalesDate AS Date, 'Rizwan' as Salesman, " _
            '     & " 'None' AS Shift, sales.[Vehicle No] as VehicleNo, Sales.Driver,'Upper Mall' as Sector,0 as ReturnPercent, " _
            '     & " Sales.SalesDate as TimeOut, 0 as TimeIn, 0 as MileOut, 0 as MileIn, ArticleDescription as Items, isnull(sales.Rate,0) as Rate,isnull(sales.disp,0) as disp," _
            '     & " isnull(ret.ret,0) ret, 0 as Offtake, 0 as S_Ret, isnull(damage.dmg,0) as Unsold, 0 as Bal, 0 as Amount, isnull(sales.PreviousBalance,0) as PreviousBalance,  " _
            '     & " 0 as PayableCash, isnull(Sales.SampleQty,0)  as Sampling, " _
            '     & " 0 as NetPayable, isnull(sales.FuelExpense,0) as Petrol, isnull(sales.Adjustment,0) Adjustment , 0 as CurrentAmount,  ((Sales.CurrentPrice - Sales.Rate) * Sales.disp) as MarketingCommission,  " _
            '     & " 0 as CashPaid, isnull(sales.OtherExpense,0)  as ToolTax , 0 as CurrentBalance,  isnull(sales.CurrentPrice,0) as CurrentPrice,   ArticleID  " _
            '     & " from articleDefTable left outer join " _
            '     & " ( SELECT     SalesMasterTable.SalesNo AS [Serial No], SalesMasterTable.SalesDate, SalesMasterTable.BiltyNo AS [Vehicle No],  " _
            '     & "                   tblDefTransporter.TransporterName AS Driver,  SalesDetailTable.Price AS Rate, " _
            '     & "               SalesDetailTable.Qty AS disp, 0 AS S_Ret, SalesMasterTable.PreviousBalance, SalesDetailTable.SampleQty,  " _
            '     & " SalesMasterTable.FuelExpense, SalesMasterTable.Adjustment, SalesMasterTable.OtherExpense,  " _
            '     & " SalesDetailTable.CurrentPrice, articleDefID " _
            '     & " FROM         SalesMasterTable INNER JOIN " _
            '     & "                   SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId  LEFT OUTER JOIN " _
            '     & "               tblDefTransporter ON SalesMasterTable.TransporterId = tblDefTransporter.TransporterId  " _
            '     & " where convert(datetime , convert(varchar, left(SalesMasterTable.SalesDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue & ")as Sales " _
            '     & " on articleDefTable.articleid = Sales.articledefid left outer join  " _
            '     & " ( SELECT     sum(SalesReturnDetailTable.Qty) AS Dmg, articleDefID " _
            '     & " FROM         SalesReturnMasterTable INNER JOIN  " _
            '     & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join  " _
            '     & " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID   " _
            '     & " WHERE       convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue & "  and ISNULL(dbo.tblDefLocation.location_type, '') <> 'Damage' group by articleDefID) as Damage " _
            '     & "  on articleDefTable.articleid = Damage.articledefid left outer join  " _
            '     & " ( SELECT     sum(SalesReturnDetailTable.Qty)  AS Ret, articleDefID  " _
            '     & " FROM         SalesReturnMasterTable INNER JOIN  " _
            '     & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join  " _
            '     & " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID   " _
            '     & " WHERE       convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue _
            '     & "  and tbldeflocation.location_type = 'Damage'" _
            '     & " group by articleDefID) as Ret  on articleDefTable.articleid = Ret.articledefid  WHERE    (Ret.Ret <> 0) OR (Damage.Dmg <> 0) OR (Sales.disp <> 0) " '(ISNULL(Sales.[Serial No], '') <> '')" ' (Sales.disp <> 0) "


            'strSQL = " Select Sales.[Serial No] AS [SerialNo],  Sales.SalesDate AS Date, 'Rizwan' as Salesman, " _
            '                 & " 'None' AS Shift, sales.[Vehicle No] as VehicleNo, Sales.Driver,'Upper Mall' as Sector,0 as ReturnPercent, " _
            '                 & " Sales.SalesDate as TimeOut, 0 as TimeIn, 0 as MileOut, 0 as MileIn, ArticleDescription as Items, isnull(sales.Rate,0) as Rate,isnull(sales.disp,0) as disp," _
            '                 & " isnull(ret.ret,0) ret, 0 as Offtake, 0 as S_Ret, isnull(damage.dmg,0) as Unsold, 0 as Bal, 0 as Amount, isnull(sales.PreviousBalance,0) as PreviousBalance,  " _
            '                 & " 0 as PayableCash, isnull(Sales.SampleQty,0)  as Sampling, " _
            '                 & " 0 as NetPayable, isnull(sales.FuelExpense,0) as Petrol, isnull(sales.Adjustment,0) Adjustment , 0 as CurrentAmount,  ((Sales.CurrentPrice - Sales.Rate) * Sales.disp) as MarketingCommission,  " _
            '                 & " 0 as CashPaid, isnull(sales.OtherExpense,0)  as ToolTax , 0 as CurrentBalance,  isnull(sales.CurrentPrice,0) as CurrentPrice, ArticleID, Sales.UserName  " _
            '                 & " from articleDefTable left outer join " _
            '                 & " ( SELECT SalesMasterTable.SalesNo AS [Serial No], SalesMasterTable.SalesDate, SalesMasterTable.BiltyNo AS [Vehicle No],  " _
            '                 & "                   tblDefTransporter.TransporterName AS Driver,  SalesDetailTable.Price AS Rate, " _
            '                 & "               SalesDetailTable.Qty AS disp, 0 AS S_Ret, SalesMasterTable.PreviousBalance, SalesDetailTable.SampleQty,  " _
            '                 & " SalesMasterTable.FuelExpense, SalesMasterTable.Adjustment, SalesMasterTable.OtherExpense,  " _
            '                 & " SalesDetailTable.CurrentPrice, articleDefID, SalesMasterTable.UserName " _
            '                 & " FROM         SalesMasterTable INNER JOIN " _
            '                 & "                   SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId  LEFT OUTER JOIN " _
            '                 & "               tblDefTransporter ON SalesMasterTable.TransporterId = tblDefTransporter.TransporterId  " _
            '                 & " where convert(datetime , convert(varchar, left(SalesMasterTable.SalesDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue & ")as Sales " _
            '                 & " on articleDefTable.articleid = Sales.articledefid left outer join  " _
            '                 & " ( SELECT     sum(SalesReturnDetailTable.Qty) AS Dmg, articleDefID " _
            '                 & " FROM         SalesReturnMasterTable INNER JOIN  " _
            '                 & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join  " _
            '                 & " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID   " _
            '                 & " WHERE       convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue & "  and ISNULL(dbo.tblDefLocation.location_type, '') <> 'Damage' group by articleDefID) as Damage " _
            '                 & "  on articleDefTable.articleid = Damage.articledefid left outer join  " _
            '                 & " ( SELECT     sum(SalesReturnDetailTable.Qty)  AS Ret, articleDefID  " _
            '                 & " FROM  SalesReturnMasterTable INNER JOIN  " _
            '                 & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join  " _
            '                 & " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID   " _
            '                 & " WHERE       convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue _
            '                 & "  and tbldeflocation.location_type = 'Damage'" _
            '                 & " group by articleDefID) as Ret  on articleDefTable.articleid = Ret.articledefid  WHERE    (Ret.Ret <> 0) OR (Damage.Dmg <> 0) OR (Sales.disp <> 0) " '(ISNULL(Sales.[Serial No], '') <> '')" ' (Sales.disp <> 0) "


            'dtMain = GetDataTable(strSQL)

            'strSQL = " SELECT  tblCustomer.CustomerName, tblListTerritory.TerritoryName , isnull(cs.CashPaid ,0) as CashPaid" _
            '        & " FROM  tblCustomer INNER JOIN" _
            '        & " tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId left outer join " _
            '        & " ( SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid " _
            '        & " FROM         tblVoucher INNER JOIN " _
            '        & "                   tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " _
            '        & " WHERE   (dbo.tblVoucher.voucher_type_id = 3 Or dbo.tblVoucher.voucher_type_id=5) AND  (convert(datetime, convert(varchar, left(tblVoucher.voucher_date,11),102),102) ='" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "' ) AND  " _
            '        & "  tblVoucherDetail.coa_detail_id = " & Me.cmbSalesman.SelectedValue _
            '        & " AND tblVoucherDetail.credit_amount > 0 " _
            '        & " GROUP BY tblVoucherDetail.coa_detail_id ) as cs on tblcustomer.accountid = cs.Accid " _
            '        & "             WHERE(tblCustomer.accountid = " & Me.cmbSalesman.SelectedValue & ")"

            'strSQL = "Select SerialNo, [Date],[Account Code], Salesman, Shift, VehicleNo, Driver, Sector, ReturnPercent, TimeOut, TimeIn, MileOut, MileIn, Items, Rate, Disp, Ret, OffTake, S_ret, Unsold, Bal, Amount, PreviousBalance, PayableCash, Sampling, NetPayable, Petrol, Adjustment, CurrentAmount, MarketingCommission, CashPaid, ToolTax, CurrentBalance, CurrentPrice, abc.ArticleId, UserName, vwArt.articleGenderName From (  " _
            '                 & " Select Sales.[Serial No] AS [SerialNo],  Sales.SalesDate AS Date, '00000' as [Account Code], 'Rizwan' as Salesman, " _
            '                 & " 'None' AS Shift, sales.[Vehicle No] as VehicleNo, Sales.Driver,'Upper Mall' as Sector,0 as ReturnPercent, " _
            '                 & " Sales.SalesDate as TimeOut, 0 as TimeIn, 0 as MileOut, 0 as MileIn, ArticleDescription as Items, isnull(sales.Rate,0) as Rate,isnull(sales.disp,0) as disp," _
            '                 & " isnull(ret.ret,0) ret, 0 as Offtake, 0 as S_Ret, isnull(damage.dmg,0) as Unsold, 0 as Bal, 0 as Amount, isnull(sales.PreviousBalance,0) as PreviousBalance,  " _
            '                 & " 0 as PayableCash, isnull(Sales.SampleQty,0)  as Sampling, " _
            '                 & " 0 as NetPayable, isnull(sales.FuelExpense,0) as Petrol, isnull(sales.Adjustment,0) Adjustment , 0 as CurrentAmount,  ((Sales.CurrentPrice - Sales.Rate) * Sales.disp) as MarketingCommission,  " _
            '                 & " 0 as CashPaid, isnull(sales.OtherExpense,0)  as ToolTax , 0 as CurrentBalance,  isnull(sales.CurrentPrice,0) as CurrentPrice, ArticleID, Sales.UserName  " _
            '                 & " from articleDefTable left outer join " _
            '                 & " ( SELECT SalesMasterTable.SalesNo AS [Serial No], SalesMasterTable.SalesDate, SalesMasterTable.BiltyNo AS [Vehicle No],  " _
            '                 & "                   tblDefTransporter.TransporterName AS Driver,  SalesDetailTable.Price AS Rate, " _
            '                 & "               Sum(SalesDetailTable.Qty) AS disp, 0 AS S_Ret, SalesMasterTable.PreviousBalance, SUM(SalesDetailTable.SampleQty) as SampleQty,  " _
            '                 & " SalesMasterTable.FuelExpense, SalesMasterTable.Adjustment, SalesMasterTable.OtherExpense,  " _
            '                 & " SalesDetailTable.CurrentPrice, articleDefID, SalesMasterTable.UserName " _
            '                 & " FROM         SalesMasterTable INNER JOIN " _
            '                 & "                   SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId  LEFT OUTER JOIN " _
            '                 & "               tblDefTransporter ON SalesMasterTable.TransporterId = tblDefTransporter.TransporterId  " _
            '                 & " where convert(datetime , convert(varchar, left(SalesMasterTable.SalesDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue & " Group By SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, SalesMasterTable.BiltyNo, tblDefTransporter.TransporterName,  SalesDetailTable.Price, SalesMasterTable.PreviousBalance, SalesMasterTable.FuelExpense, SalesMasterTable.Adjustment, SalesMasterTable.OtherExpense, SalesDetailTable.CurrentPrice, articleDefID, SalesMasterTable.UserName)as Sales " _
            '                 & " on articleDefTable.articleid = Sales.articledefid left outer join  " _
            '                 & " ( SELECT     sum(SalesReturnDetailTable.Qty) AS Dmg, articleDefID " _
            '                 & " FROM         SalesReturnMasterTable INNER JOIN  " _
            '                 & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join  " _
            '                 & " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID   " _
            '                 & " WHERE       convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue & "  and ISNULL(dbo.tblDefLocation.location_type, '') <> 'Damage' group by articleDefID) as Damage " _
            '                 & "  on articleDefTable.articleid = Damage.articledefid left outer join  " _
            '                 & " ( SELECT     sum(SalesReturnDetailTable.Qty)  AS Ret, articleDefID  " _
            '                 & " FROM  SalesReturnMasterTable INNER JOIN  " _
            '                 & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join  " _
            '                 & " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID   " _
            '                 & " WHERE       convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue _
            '                 & "  and tbldeflocation.location_type = 'Damage'" _
            '                 & " group by articleDefID) as Ret  on articleDefTable.articleid = Ret.articledefid  WHERE    (Ret.Ret <> 0) OR (Damage.Dmg <> 0) OR (Sales.disp <> 0) ) abc Inner join ArticleDefView vwArt on abc.ArticleId = vwArt.ArticleId ORDER BY vwArt.SortOrder ASC" '(ISNULL(Sales.[Serial No], '') <> '')" ' (Sales.disp <> 0) "
            'dtMain = GetDataTable(strSQL)



            'strSQL = "Select SerialNo, [Date],[Account Code], Salesman, Shift, VehicleNo, Driver, Sector, ReturnPercent, TimeOut, TimeIn, MileOut, MileIn, Items,  Rate, Disp, Ret, OffTake, S_ret, Unsold, Bal, Amount, PreviousBalance, PayableCash, Sampling, NetPayable, Petrol, Adjustment, CurrentAmount, MarketingCommission, CashPaid, ToolTax, CurrentBalance, CurrentPrice, abc.ArticleId, UserName, vwArt.articleGenderName,0 as PrintCont  From (  " _
            '                 & " Select Sales.[Serial No] AS [SerialNo],  Sales.SalesDate AS Date, '00000' as [Account Code], 'Rizwan' as Salesman, " _
            '                 & " 'None' AS Shift, sales.[Vehicle No] as VehicleNo, Sales.Driver,'Upper Mall' as Sector,0 as ReturnPercent, " _
            '                 & " Sales.SalesDate as TimeOut, 0 as TimeIn, 0 as MileOut, 0 as MileIn, ArticleDescription as Items, isnull(sales.Rate,0) as Rate,isnull(sales.disp,0) as disp," _
            '                 & " isnull(ret.ret,0) ret, 0 as Offtake, 0 as S_Ret, isnull(damage.dmg,0) as Unsold, 0 as Bal, 0 as Amount, isnull(sales.PreviousBalance,0) as PreviousBalance,  " _
            '                 & " 0 as PayableCash, isnull(Sales.SampleQty,0)  as Sampling, " _
            '                 & " 0 as NetPayable, isnull(sales.FuelExpense,0) as Petrol, isnull(sales.Adjustment,0) Adjustment , 0 as CurrentAmount,  ((Sales.CurrentPrice - Sales.Rate) * Sales.disp) as MarketingCommission,  " _
            '                 & " 0 as CashPaid, isnull(sales.OtherExpense,0)  as ToolTax , 0 as CurrentBalance,  isnull(Sales.CurrentPrice,0) as CurrentPrice, ArticleID, Sales.UserName " _
            '                 & " from articleDefTable left outer join " _
            '                 & " (Select DISTINCT SalesDt.[Serial No] as [Serial No], Salesdt.[Vehicle No], SalesDt.[Driver], SalesDate, Rate, Disp, S_Ret, SampleQty, CurrentPrice, ArticleDefId, SalesDt.UserName, abc.Customercode, isnull(adj.FuelExpense, 0) as FuelExpense, isnull(adj.OtherExpense,0) as OtherExpense, isnull(adj.Adjustment,0) as Adjustment, Isnull(Opening.PreviousBalance,0) as PreviousBalance From (" _
            '                 & " SELECT SalesMasterTable.CustomerCode, '' AS [Serial No],  Convert(DateTime, Convert(Varchar, LEFT(SalesDate,11), 102),102) as SalesDate,    " _
            '                 & " SalesDetailTable.Price AS Rate,  " _
            '                 & " Sum(SalesDetailTable.Qty) AS disp, 0 AS S_Ret,SUM(SalesDetailTable.SampleQty) as SampleQty,   " _
            '                 & " SalesDetailTable.CurrentPrice, articleDefID,  '' as UserName  " _
            '                 & " FROM SalesMasterTable INNER JOIN  " _
            '                 & " SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId  LEFT OUTER JOIN  " _
            '                 & " tblDefTransporter ON SalesMasterTable.TransporterId = tblDefTransporter.TransporterId   " _
            '                 & " where convert(datetime , convert(varchar, left(SalesMasterTable.SalesDate,11),102),102) = Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)  and customerCode =" & Me.cmbSalesman.SelectedValue & "   Group By  SalesMasterTable.CustomerCode,Convert(DateTime, Convert(Varchar, LEFT(SalesDate,11), 102),102), SalesDetailTable.Price, SalesDetailTable.CurrentPrice, articleDefID " _
            '                 & " ) abc LEFT OUTER JOIN (Select CustomerCode, SUM(isnull(FuelExpense,0)) as FuelExpense, SUM(isnull(OtherExpense,0)) as OtherExpense, SUM(isnull(Adjustment,0)) as Adjustment From SalesMasterTable WHERE Convert(DateTime, Convert(Varchar, LEFT(SalesDate,11),102),102) = Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND CustomerCode=" & Me.cmbSalesman.SelectedValue & " Group by CustomerCode) Adj on Adj.CustomerCode = abc.Customercode " _
            '                 & " LEFT OUTER JOIN (Select tblvoucherdetail.coa_detail_id, SUM(debit_amount-credit_amount) as PreviousBalance From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.voucher_id = tblvoucherdetail.voucher_id WHERE Convert(DateTime, Convert(Varchar, LEFT(Voucher_Date, 11),102),102) < Convert(dateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND tblVoucherDetail.coa_detail_id=" & Me.cmbSalesman.SelectedValue & " group by tblvoucherdetail.coa_detail_id) Opening on Opening.coa_detail_id = abc.CustomerCode " _
            '                 & " LEFT OUTER JOIN (Select CustomerCode, SalesNo as [Serial No], UserName, BiltyNo as [Vehicle No], tblDefTransporter.TransporterName as Driver From SalesMasterTable LEFT OUTER JOIN tblDefTransporter on tblDefTransporter.TransporterId = SalesMasterTable.TransporterId WHERE SalesId in(Select Max(SalesId) From SalesMasterTable WHERE Customercode=" & Me.cmbSalesman.SelectedValue & " AND Convert(DateTime, Convert(Varchar, LEFT(SalesDate,11),102),102)=Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND Customercode=" & Me.cmbSalesman.SelectedValue & " AND Convert(DateTime, Convert(Varchar, LEFT(SalesDate,11),102),102)=Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) ) SalesDt on SalesDt.CustomerCode = abc.CustomerCode " _
            '                 & " )as Sales " _
            '                 & " on articleDefTable.articleid = Sales.articledefid left outer join  " _
            '                 & " ( SELECT     sum(SalesReturnDetailTable.Qty) AS Dmg, articleDefID " _
            '                 & " FROM         SalesReturnMasterTable INNER JOIN  " _
            '                 & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join  " _
            '                 & " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID   " _
            '                 & " WHERE       convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue & "  and ISNULL(dbo.tblDefLocation.location_type, '') <> 'Damage' group by articleDefID) as Damage " _
            '                 & "  on articleDefTable.articleid = Damage.articledefid left outer join  " _
            '                 & " ( SELECT     sum(SalesReturnDetailTable.Qty)  AS Ret, articleDefID  " _
            '                 & " FROM  SalesReturnMasterTable INNER JOIN  " _
            '                 & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join  " _
            '                 & " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID   " _
            '                 & " WHERE       convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue _
            '                 & "  and tbldeflocation.location_type = 'Damage'" _
            '                 & " group by articleDefID) as Ret  on articleDefTable.articleid = Ret.articledefid  WHERE    (Ret.Ret <> 0) OR (Damage.Dmg <> 0) OR (Sales.disp <> 0) ) abc Inner join ArticleDefView vwArt on abc.ArticleId = vwArt.ArticleId ORDER BY vwArt.SortOrder ASC" '(ISNULL(Sales.[Serial No], '') <> '')" ' (Sales.disp <> 0) "
            'dtMain = GetDataTable(strSQL)
            strSQL = "Select SerialNo, [Date],[Account Code], Salesman, Shift, VehicleNo, Driver, Sector, ReturnPercent, TimeOut, TimeIn, MileOut, MileIn, Items,  Rate, Disp, Ret, OffTake, S_ret, Unsold, Bal, Amount, PreviousBalance, PayableCash, Sampling, NetPayable, Petrol, Adjustment, CurrentAmount, MarketingCommission, CashPaid, ToolTax, CurrentBalance, CurrentPrice, abc.ArticleId, UserName, vwArt.articleGenderName,0 as PrintCont, IsNull(vwArt.ItemWeight,0) as Item_Weight, vwArt.ArticleTypeName, IsNull(Sz7,0) as Sz7   From (  " _
                                         & " Select Sales.[Serial No] AS [SerialNo],  Sales.SalesDate AS Date, '00000' as [Account Code], 'Rizwan' as Salesman, " _
                                         & " 'None' AS Shift, sales.[Vehicle No] as VehicleNo, Sales.Driver,'Upper Mall' as Sector,0 as ReturnPercent, " _
                                         & " Sales.SalesDate as TimeOut, 0 as TimeIn, 0 as MileOut, 0 as MileIn, ArticleDescription as Items, isnull(sales.Rate,0) as Rate,isnull(sales.disp,0) as disp," _
                                         & " isnull(ret.ret,0) ret, 0 as Offtake, 0 as S_Ret, isnull(damage.dmg,0) as Unsold, 0 as Bal, 0 as Amount, isnull(sales.PreviousBalance,0) as PreviousBalance,  " _
                                         & " 0 as PayableCash, isnull(Sales.SampleQty,0)  as Sampling, " _
                                         & " 0 as NetPayable, isnull(sales.FuelExpense,0) as Petrol, isnull(sales.Adjustment,0) Adjustment , 0 as CurrentAmount,  ((Sales.CurrentPrice - Sales.Rate) * Sales.disp) as MarketingCommission,  " _
                                         & " 0 as CashPaid, isnull(sales.OtherExpense,0)  as ToolTax , 0 as CurrentBalance,  isnull(Sales.CurrentPrice,0) as CurrentPrice, ArticleID, Sales.UserName, IsNull(Sales.Sz7,0) as Sz7 " _
                                         & " from articleDefTable left outer join " _
                                         & " (Select DISTINCT SalesDt.[Serial No] as [Serial No], Salesdt.[Vehicle No], SalesDt.[Driver], SalesDate, Rate, Disp, S_Ret, SampleQty, IsNull(Sz7,0) as Sz7, CurrentPrice, ArticleDefId, SalesDt.UserName, abc.Customercode, isnull(adj.FuelExpense, 0) as FuelExpense, isnull(adj.OtherExpense,0) as OtherExpense, isnull(adj.Adjustment,0) as Adjustment, Isnull(Opening.PreviousBalance,0) as PreviousBalance From (" _
                                         & " SELECT SalesMasterTable.CustomerCode, '' AS [Serial No],  Convert(DateTime, Convert(Varchar, LEFT(SalesDate,11), 102),102) as SalesDate,    " _
                                         & " SalesDetailTable.Price AS Rate,  " _
                                         & " Sum(SalesDetailTable.Qty) AS disp, 0 AS S_Ret,SUM(SalesDetailTable.SampleQty) as SampleQty, Sum(IsNull(dbo.SalesDetailTable.Sz1,0)) as Sz7,   " _
                                         & " SalesDetailTable.CurrentPrice, articleDefID,  '' as UserName  " _
                                         & " FROM SalesMasterTable INNER JOIN  " _
                                         & " SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId  LEFT OUTER JOIN  " _
                                         & " tblDefTransporter ON SalesMasterTable.TransporterId = tblDefTransporter.TransporterId   " _
                                         & " where convert(datetime , convert(varchar, left(SalesMasterTable.SalesDate,11),102),102) = Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)  and customerCode =" & Me.cmbSalesman.SelectedValue & "   Group By  SalesMasterTable.CustomerCode,Convert(DateTime, Convert(Varchar, LEFT(SalesDate,11), 102),102), SalesDetailTable.Price, SalesDetailTable.CurrentPrice, articleDefID " _
                                         & " ) abc LEFT OUTER JOIN (Select CustomerCode, SUM(isnull(FuelExpense,0)) as FuelExpense, SUM(isnull(OtherExpense,0)) as OtherExpense, SUM(isnull(Adjustment,0)) as Adjustment From SalesMasterTable WHERE Convert(DateTime, Convert(Varchar, LEFT(SalesDate,11),102),102) = Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND CustomerCode=" & Me.cmbSalesman.SelectedValue & " Group by CustomerCode) Adj on Adj.CustomerCode = abc.Customercode " _
                                         & " LEFT OUTER JOIN (Select tblvoucherdetail.coa_detail_id, SUM(debit_amount-credit_amount) as PreviousBalance From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.voucher_id = tblvoucherdetail.voucher_id WHERE Convert(DateTime, Convert(Varchar, LEFT(Voucher_Date, 11),102),102) < Convert(dateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND tblVoucherDetail.coa_detail_id=" & Me.cmbSalesman.SelectedValue & " group by tblvoucherdetail.coa_detail_id) Opening on Opening.coa_detail_id = abc.CustomerCode " _
                                         & " LEFT OUTER JOIN (Select CustomerCode, SalesNo as [Serial No], UserName, BiltyNo as [Vehicle No], tblDefTransporter.TransporterName as Driver From SalesMasterTable LEFT OUTER JOIN tblDefTransporter on tblDefTransporter.TransporterId = SalesMasterTable.TransporterId WHERE SalesId in(Select Max(SalesId) From SalesMasterTable WHERE Customercode=" & Me.cmbSalesman.SelectedValue & " AND Convert(DateTime, Convert(Varchar, LEFT(SalesDate,11),102),102)=Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND Customercode=" & Me.cmbSalesman.SelectedValue & " AND Convert(DateTime, Convert(Varchar, LEFT(SalesDate,11),102),102)=Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) ) SalesDt on SalesDt.CustomerCode = abc.CustomerCode " _
                                         & " )as Sales " _
                                         & " on articleDefTable.articleid = Sales.articledefid left outer join  " _
                                         & " ( SELECT     sum(SalesReturnDetailTable.Qty) AS Dmg, articleDefID " _
                                         & " FROM         SalesReturnMasterTable INNER JOIN  " _
                                         & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join  " _
                                         & " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID   " _
                                         & " WHERE       convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue & "  and ISNULL(dbo.tblDefLocation.location_type, '') <> 'Damage' group by articleDefID) as Damage " _
                                         & "  on articleDefTable.articleid = Damage.articledefid left outer join  " _
                                         & " ( SELECT     sum(SalesReturnDetailTable.Qty)  AS Ret, articleDefID  " _
                                         & " FROM  SalesReturnMasterTable INNER JOIN  " _
                                         & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join  " _
                                         & " tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID   " _
                                         & " WHERE       convert(datetime , convert(varchar, left(SalesReturnMasterTable.SalesReturnDate,11),102),102) = '" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "'  and customerCode = " & Me.cmbSalesman.SelectedValue _
                                         & "  and tbldeflocation.location_type = 'Damage'" _
                                         & " group by articleDefID) as Ret  on articleDefTable.articleid = Ret.articledefid  WHERE    (Ret.Ret <> 0) OR (Damage.Dmg <> 0) OR (Sales.disp <> 0) ) abc Inner join ArticleDefView vwArt on abc.ArticleId = vwArt.ArticleId ORDER BY vwArt.SortOrder ASC" '(ISNULL(Sales.[Serial No], '') <> '')" ' (Sales.disp <> 0) "
            dtMain = GetDataTable(strSQL)

            strSQL = " SELECT  tblCustomer.CustomerCode, tblCustomer.CustomerName, tblListTerritory.TerritoryName , isnull(cs.CashPaid ,0) as CashPaid " _
                    & " FROM  tblCustomer INNER JOIN" _
                    & " tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId  LEFT OUTER JOIN vwCOAdetail on vwCOADetail.coa_detail_id = tblCustomer.AccountId left outer join " _
                    & " ( SELECT     SUM(tblVoucherDetail.credit_amount) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid " _
                    & " FROM         tblVoucher INNER JOIN " _
                    & "                   tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id " _
                    & " WHERE   (dbo.tblVoucher.voucher_type_id = 3 Or dbo.tblVoucher.voucher_type_id=5) AND  (convert(datetime, convert(varchar, left(tblVoucher.voucher_date,11),102),102) ='" & Me.dtpFrom.Value.Date.ToString("yyyy-MM-dd") & "' ) AND  " _
                    & "  tblVoucherDetail.coa_detail_id = " & Me.cmbSalesman.SelectedValue _
                    & " AND tblVoucherDetail.credit_amount > 0 " _
                    & " GROUP BY tblVoucherDetail.coa_detail_id ) as cs on tblcustomer.accountid = cs.Accid " _
                    & "             WHERE(tblCustomer.accountid = " & Me.cmbSalesman.SelectedValue & ")"

            Dim dt As DataTable = GetDataTable(strSQL)
            If dt.Rows.Count > 0 Then
                dtMain.Columns("Account Code").Expression = "'" & dt.Rows(0)(0) & "'"
                dtMain.Columns("Salesman").Expression = "'" & dt.Rows(0)(1) & "'"
                dtMain.Columns("Sector").Expression = "'" & dt.Rows(0)(2) & "'"
                dtMain.Columns("CashPaid").Expression = dt.Rows(0)(3)
                dtMain.Columns("PreviousBalance").Expression = Val(GetAccountBalance(Me.cmbSalesman.SelectedValue, Me.dtpFrom.Value.Date.AddDays(-1)).ToString)
                dtMain.Columns("PrintCont").Expression = intPrintCont
            End If

            Return dtMain

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            ShowReport("rptSalesmandealerVoucher", , , , True, , , Me.MakeDataTable(), , rptReportViewerNew.enmReport.SalesmanVoucher)
            PrintCount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function getUserGroup(ByVal UserId As Integer) As String
        Try
            Dim str As String = String.Empty
            str = "Select GroupType From tblUserGroup WHERE GroupId in(Select GroupId From tblUser WHERE User_Id=" & UserId & ")"
            Dim dt As New DataTable
            dt = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function getPrintCont(ByVal SaleManId As Integer, ByVal VoucherDate As DateTime) As Integer
        Try
            Dim str As String = String.Empty
            str = "Select Count(Isnull(SaleManId,0)) as Cont From tblPrintVoucherLog WHERE SaleManId=" & SaleManId & " AND (Convert(Varchar, VoucherDate,102)=Convert(DateTime, '" & VoucherDate.ToString("yyyy-M-d 00:00:00") & "', 102)) Group by SaleManId "
            Dim dt As New DataTable
            dt = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0) + 1
                Else
                    Return 1
                End If
            Else
                Return 1
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub PrintCount()
        Try
            If flgPrintLog = True Then
                Dim PrintLog As New SBModel.PrintVoucherLogBE
                PrintLog.SaleManId = Me.cmbSalesman.SelectedValue
                PrintLog.VoucherDate = Me.dtpFrom.Value
                PrintLog.UserName = LoginUserName
                Call SBDal.PrintVoucherLogDAL.Save(PrintLog)

                intPrintCont = getPrintCont(Me.cmbSalesman.SelectedValue, Me.dtpFrom.Value)
                If Not UserGroup = "Administrator" Then
                    If PrintCont > intPrintCont Then
                        intPrintCont = intPrintCont
                    Else
                        ShowErrorMessage("Print not allowed")
                        Exit Sub
                    End If
                Else
                    intPrintCont = intPrintCont
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class