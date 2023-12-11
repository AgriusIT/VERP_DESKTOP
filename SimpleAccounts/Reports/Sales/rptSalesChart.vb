Public Class rptSalesChart
    Implements IGeneral


    Enum EnumGrid
        Name
        Sector
        Dispatch
        Ret
        Unsold
        RetPer
        Offtake
        S_Ret
        MC
        Sampling
        Adjsmt
        Petrol
        Tooltax
        Cashpaid
        NetSales
        Days
    End Enum
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateReport.Click
        Try
            Dim strSQL As New System.Text.StringBuilder
            strSQL.AppendLine("")
            strSQL.AppendLine(" select CustomerName as [Name], tbllistTerritory.TerritoryName as Sector, isnull(dispatch.dispatch,0) as Dispatch , isnull(Damage.Dmg,0) as Ret, isnull(ret.ret,0) as Unsold ,  ")
            strSQL.AppendLine(" Case When isnull(dispatch.dispatch,1) <> 0 then Round((isnull(Damage.Dmg,0) * 100)/ isnull(dispatch.dispatch,1),0) Else 0 End as [Ret%] , Round(isnull(dispatch.dispatch,0) - (isnull(ret.ret,0)+IsNull(Damage.Dmg,0)),0) as [Offtake], ")
            strSQL.AppendLine("  0 as S_ret,  isnull(dispatch.mc,0) - (IsNull(Damage.ReturnMc,0) + IsNull(Ret.UnsoldMc,0)) as MC, isnull(dispatch.Sampling,0) as Sampling, ")
            strSQL.AppendLine(" isnull(Adjustment.Adjustment,0) as Adjsmt,  isnull(FuelExp.FuelExpense,0) as Petrol, isnull(OtherExp.OtherExpense,0) as Tooltax, ")
            strSQL.AppendLine(" isnull(cs.CashPaid,0) as [Cash Paid], isnull(dispatch.dispatch,0) - (isnull(ret.ret,0) + IsNull(Damage.Dmg,0) + isnull(dispatch.mc,0) + ")
            strSQL.AppendLine(" isnull(Adjustment.Adjustment,0) + isnull(OtherExp.OtherExpense,0) + isnull(FuelExp.FuelExpense,0)) as NetSales, isnull(Days.Days,0) as [#.Of Days] ")
            strSQL.AppendLine(" from tblcustomer  left outer join  ")
            strSQL.AppendLine(" (SELECT Round(SUM(SalesDetailTable.Qty * SalesDetailTable.CurrentPrice),2) AS dispatch, CustomerCode, sum((SalesDetailTable.CurrentPrice - price)* SalesDetailTable.Qty) as MC,  ")
            strSQL.AppendLine(" sum(isnull(SampleQty,0)) as Sampling ")
            strSQL.AppendLine(" FROM SalesDetailTable INNER JOIN ")
            strSQL.AppendLine("   SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId ")
            strSQL.AppendLine(" WHERE     (convert(varchar , SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            strSQL.AppendLine(" GROUP BY CustomerCode) as dispatch on tblcustomer.accountid = dispatch.CustomerCode LEFT OUTER JOIN ")
            strSQL.AppendLine(" (Select CustomerCode, Count(Distinct SalesDate) as Days From  SalesMasterTable WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) Group By CustomerCode) Days On  tblCustomer.AccountId = Days.CustomerCode left outer join ")
            strSQL.AppendLine(" (Select CustomerCode, Sum(FuelExpense) as FuelExpense From  SalesMasterTable WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) Group By CustomerCode) FuelExp On  tblCustomer.AccountId = FuelExp.CustomerCode left outer join ")
            strSQL.AppendLine(" (Select CustomerCode, Sum(OtherExpense) as OtherExpense From  SalesMasterTable WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) Group By CustomerCode) OtherExp On  tblCustomer.AccountId = OtherExp.CustomerCode left outer join ")
            strSQL.AppendLine(" (Select CustomerCode, Sum(Adjustment) as Adjustment From  SalesMasterTable WHERE     (convert(datetime, convert(varchar, left(salesMasterTable.SalesDate,11),102),102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) Group By CustomerCode) Adjustment On  tblCustomer.AccountId = Adjustment.CustomerCode left outer join ")
            strSQL.AppendLine(" 	   (SELECT     Round(SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price),0) AS Ret, CustomerCode, SUM((IsNull(SalesReturnDetailTable.CurrentPrice,0)-IsNull(SalesReturnDetailTable.Price,0)) * IsNull(SalesReturnDetailTable.Qty,0)) As UnsoldMc ")
            strSQL.AppendLine(" FROM         SalesReturnMasterTable INNER JOIN ")
            strSQL.AppendLine("   SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join ")
            strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID ")
            strSQL.AppendLine(" WHERE     ( convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            strSQL.AppendLine(" AND tbldeflocation.location_type <> 'Damage' ")
            strSQL.AppendLine(" GROUP BY CustomerCode) as Ret      on tblcustomer.accountid = ret.CustomerCode left outer join ")
            strSQL.AppendLine(" 	  (SELECT     SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Dmg, CustomerCode, SUM((IsNull(SalesReturnDetailTable.CurrentPrice,0)-IsNull(SalesReturnDetailTable.Price,0)) * IsNull(SalesReturnDetailTable.Qty,0)) AS ReturnMc ")
            strSQL.AppendLine(" FROM         SalesReturnMasterTable INNER JOIN ")
            strSQL.AppendLine("   SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join ")
            strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  ")
            strSQL.AppendLine(" WHERE     ( convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            strSQL.AppendLine(" AND tbldeflocation.location_type = 'Damage'		 ")
            strSQL.AppendLine(" GROUP BY CustomerCode) as Damage  on tblcustomer.accountid = Damage.CustomerCode left outer join ")
            strSQL.AppendLine(" (SELECT   SUM(IsNull(tblVoucherDetail.credit_amount,0)) AS CashPaid, tblVoucherDetail.coa_detail_id AS accid ")
            strSQL.AppendLine(" FROM         tblVoucher INNER JOIN ")
            strSQL.AppendLine("   tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id ")
            strSQL.AppendLine(" WHERE     (convert(varchar, tblVoucher.voucher_date,102)  BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND  ")
            strSQL.AppendLine("   (IsNull(tblVoucherDetail.credit_amount,0) > 0) ")
            strSQL.AppendLine(" GROUP BY tblVoucherDetail.coa_detail_id ) as cs on tblcustomer.accountid = cs.Accid ")
            strSQL.AppendLine(" inner join tbllistterritory on tbllistterritory.territoryID = tblcustomer.Territory")
            strSQL.AppendLine(" WHERE tblcustomer.Active=1 ")

            Me.grdRcords.DataSource = GetDataTable(strSQL.ToString())
            Me.grdRcords.RetrieveStructure()
            Me.ApplyGridSettings()
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.grdRcords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdRcords.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdRcords.RecordNavigator = True
            Me.grdRcords.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007


            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdRcords.RootTable.Columns

                If col.Index <> EnumGrid.Name AndAlso col.Index <> EnumGrid.Sector AndAlso col.Index <> EnumGrid.RetPer AndAlso col.Index <> EnumGrid.Days Then
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    col.FormatString = String.Empty
                    col.TotalFormatString = String.Empty
                End If


            Next

            Me.grdRcords.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.grdRcords.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
            Me.grdRcords.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
            Me.grdRcords.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains



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

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Sales Chart"
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdRcords.LoadLayoutFile(fs1)
                fs1.Close()
                fs1.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Chart Report" & Chr(10) & "Date From: " & Me.dtpFrom.Value.ToString("dd-MM-yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd-Mm-yyyy") & " "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class