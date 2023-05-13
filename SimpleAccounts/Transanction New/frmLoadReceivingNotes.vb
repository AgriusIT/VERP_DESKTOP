Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmLoadReceivingNotes
    Implements IGeneral

    Dim ReceivingNoId As Integer = 0
    Dim flgMultiGRN As String

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

        If Condition = "RecvNo" Then

            FillUltraDropDown(Me.cmbReceivingNo, "select receivingnotemastertable.receivingnoteid as ReceivingNoId , receivingnotemastertable.ReceivingNo , vwCOADetail.detail_title as Vendor , receivingnotemastertable.ReceivingDate , receivingnotemastertable.IGPNo , " _
                               & "receivingnotemastertable.Remarks , receivingnotemastertable.DcNo , receivingnotemastertable.Vehicle_No as Vehicle , " _
                               & "tbldefcostcenter.Name as CostCenterName from receivingnotemastertable " _
                               & "INNER JOIN vwCOADetail on vwCOADetail.coa_Detail_id = receivingnotemastertable.vendorId " _
                               & "Left JOIN tbldefcostcenter on tbldefcostcenter.CostCenterID = receivingnotemastertable.CostCenterID " _
                               & "where receivingnotemastertable.vendorId = " & Me.cmbVendor.ActiveRow.Cells(0).Value & " And receivingnotemastertable.Status = N'" & EnumStatus.Open.ToString & "' And receivingnotemastertable.receivingnoteid Not In (select IsNull(ReceivingDetailTable.ReceivingNoteId,0) as receivingnoteid from ReceivingDetailTable) ORDER BY ReceivingNoteMasterTable.ReceivingNoteId DESC")
            Me.cmbReceivingNo.Rows(0).Activate()
            If Me.cmbReceivingNo.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbReceivingNo.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbReceivingNo.DisplayLayout.Bands(0).Columns(2).Hidden = True
                'Me.cmbReceivingNo.DisplayLayout.Bands(0).Columns(4).Hidden = True
                Me.cmbReceivingNo.DisplayLayout.Bands(0).Columns(5).Hidden = True
                Me.cmbReceivingNo.DisplayLayout.Bands(0).Columns(6).Hidden = True
            End If

        ElseIf Condition = "Vendor" Then
            Dim formName As String = "frmPurchaseNew" ''This Variable is added to get COA Account which are mapped with Sales :TFS3419
            Dim str As String
            If getConfigValueByType("Show Customer On Purchase") = True Then
                str = "Select coa_detail_id, detail_title as Vendor, detail_code as [Cdode], sub_sub_title as [Account Head], Account_Type as Type From vwCOADetail where detail_title <> '' And Account_Type in('Vendor','Customer') "
                ' FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title as Vendor, detail_code as [Cdode], sub_sub_title as [Account Head], Account_Type as Type From vwCOADetail where detail_title <> '' And Account_Type in('Vendor','Customer') Order By detail_title ASC") ''TFS3322

            Else
                str = "Select coa_detail_id, detail_title as Vendor, detail_code as [Cdode], sub_sub_title as [Account Head], Account_Type as Type From vwCOADetail where detail_title <> '' And Account_Type in('Vendor') "
                ' FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title as Vendor, detail_code as [Cdode], sub_sub_title as [Account Head], Account_Type as Type From vwCOADetail where detail_title <> '' And Account_Type in('Vendor') Order By detail_title ASC") ''TFS3322
            End If
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            If LoginGroup = "Administrator" Then
            ElseIf GetMappedUserId() > 0 And getGroupAccountsConfigforPurchase(formName) Then
                str = "Select coa_detail_id, detail_title as Vendor, detail_code as [Cdode], sub_sub_title as [Account Head], Account_Type as Type From vwCOADetail where detail_title <> ''  "
                str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ") )"
            End If
            ''End TFS3322
            str += " Order By detail_title ASC "
            FillUltraDropDown(Me.cmbVendor, str)
            Me.cmbVendor.Rows(0).Activate()
            If Me.cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
        End If

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmLoadReceivingNotes_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FillCombos("Vendor")
        FillCombos("RecvNo")

        flgMultiGRN = getConfigValueByType("LoadMultiGRN").ToString

    End Sub

    Private Sub cmbVendor_Leave(sender As Object, e As EventArgs) Handles cmbVendor.Leave

        Try
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.ActiveRow IsNot Nothing Then
                FillCombos("RecvNo")
                TxtIGPNo.Text = String.Empty
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbReceivingNo_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbReceivingNo.RowSelected

        Try

            If Me.cmbReceivingNo.IsItemInList = False Then Exit Sub
            If Me.cmbReceivingNo.ActiveRow IsNot Nothing Then

                If Me.cmbReceivingNo.ActiveRow.Cells(0).Value > 0 Then

                    TxtIGPNo.Text = cmbReceivingNo.ActiveRow.Cells(4).Value

                ElseIf Me.cmbReceivingNo.ActiveRow.Cells(0).Value <= 0 Then

                    TxtIGPNo.Text = String.Empty

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click

        If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 And Me.cmbReceivingNo.ActiveRow.Cells(0).Value <= 0 Then

            fillGrid("select receivingnotemastertable.receivingnoteid as ReceivingNoId , vwCOADetail.detail_title as Vendor , receivingnotemastertable.ReceivingNo , receivingnotemastertable.ReceivingDate , receivingnotemastertable.IGPNo , receivingnotemastertable.vendor_invoice_no As InvoiceNo , " _
                               & "receivingnotemastertable.Remarks , receivingnotemastertable.DcNo , receivingnotemastertable.Vehicle_No as Vehicle , " _
                               & "tbldefcostcenter.Name as CostCenterName , vwCOADetail.coa_Detail_id as VendorId , tbldefcostcenter.CostCenterID " _
                               & ", Driver_Name , CurrencyType , CurrencyRate , Transportation_Vendor , Custom_Vendor , Arrival_Time , Departure_Time from receivingnotemastertable " _
                               & "INNER JOIN vwCOADetail on vwCOADetail.coa_Detail_id = receivingnotemastertable.vendorId " _
                               & "LEFT JOIN tbldefcostcenter on tbldefcostcenter.CostCenterID = receivingnotemastertable.CostCenterID " _
                               & "where receivingnotemastertable.vendorId = " & Me.cmbVendor.ActiveRow.Cells(0).Value & " And receivingnotemastertable.Status = N'" & EnumStatus.Open.ToString & "' And receivingnotemastertable.receivingnoteid Not In (select IsNull(ReceivingDetailTable.ReceivingNoteId,0) as receivingnoteid from ReceivingDetailTable) ORDER BY ReceivingNoteMasterTable.ReceivingNoteId DESC")

            'ShowErrorMessage("ReceivingNo must required with Vendor")

        ElseIf Me.cmbVendor.ActiveRow.Cells(0).Value > 0 And Me.cmbReceivingNo.ActiveRow.Cells(0).Value > 0 Then

            fillGrid("select receivingnotemastertable.receivingnoteid as ReceivingNoId , vwCOADetail.detail_title as Vendor , receivingnotemastertable.ReceivingNo , receivingnotemastertable.ReceivingDate , receivingnotemastertable.IGPNo , receivingnotemastertable.vendor_invoice_no As InvoiceNo, " _
                               & "receivingnotemastertable.Remarks , receivingnotemastertable.DcNo , receivingnotemastertable.Vehicle_No as Vehicle , " _
                               & "tbldefcostcenter.Name as CostCenterName , vwCOADetail.coa_Detail_id as VendorId , tbldefcostcenter.CostCenterID " _
                               & ", Driver_Name , CurrencyType , CurrencyRate , Transportation_Vendor , Custom_Vendor , Arrival_Time , Departure_Time from receivingnotemastertable " _
                               & "INNER JOIN vwCOADetail on vwCOADetail.coa_Detail_id = receivingnotemastertable.vendorId " _
                               & "LEFT JOIN tbldefcostcenter on tbldefcostcenter.CostCenterID = receivingnotemastertable.CostCenterID " _
                               & "where receivingnotemastertable.vendorId = " & Me.cmbVendor.ActiveRow.Cells(0).Value & " And receivingnotemastertable.Status = N'" & EnumStatus.Open.ToString & "' and receivingnotemastertable.receivingnoteid = " & Me.cmbReceivingNo.ActiveRow.Cells(0).Value & " ORDER BY ReceivingNoteMasterTable.ReceivingNoteId DESC")

        End If

    End Sub

    Public Sub fillGrid(query As String)

        Try
            Dim dt As New DataTable
            dt = New PlanTicketsDAL().GetDeatilsByTickets(query)
            Me.grdItems.DataSource = dt

            Me.grdItems.RootTable.Columns("ReceivingDate").FormatString = str_DisplayDateFormat
            Me.grdItems.RootTable.Columns("CurrencyRate").FormatString = DecimalPointInValue
            Me.grdItems.RootTable.Columns("Arrival_Time").FormatString = str_DisplayDateFormat
            Me.grdItems.RootTable.Columns("Departure_Time").FormatString = str_DisplayDateFormat
            For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grdItems.RootTable.Columns
                c.EditType = Janus.Windows.GridEX.EditType.NoEdit
            Next

            'Me.grdItems.RootTable.Columns("PendingQty").FormatString = "N" & DecimalPointInValue
            'Me.grdItems.RootTable.Columns("CurrentValue").FormatString = "N" & DecimalPointInValue
            'Me.grdItems.RootTable.Columns("ClosingValue").FormatString = "N" & DecimalPointInValue
            'Me.grdItems.RootTable.Columns("AcquireCost").FormatString = "N" & DecimalPointInValue
            'Me.grdItems.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub grdItems_KeyDown(sender As Object, e As KeyEventArgs) Handles grdItems.KeyDown

        Try

            Dim dt As New DataTable

            dt.Columns.Add("ReceivingNoId")
            dt.Columns.Add("Vendor")
            dt.Columns.Add("ReceivingNo")
            dt.Columns.Add("ReceivingDate")
            dt.Columns.Add("IGPNo")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("DcNo")
            dt.Columns.Add("Vehicle")
            dt.Columns.Add("CostCenterName")
            dt.Columns.Add("VendorId")
            dt.Columns.Add("CostCenterID")
            dt.Columns.Add("Driver_Name")
            dt.Columns.Add("CurrencyType")
            dt.Columns.Add("CurrencyRate")
            dt.Columns.Add("Transportation_Vendor")
            dt.Columns.Add("Custom_Vendor")
            dt.Columns.Add("Arrival_Time")
            dt.Columns.Add("Departure_Time")
            dt.Columns.Add("InvoiceNo")

            For Each row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows

                Dim R As DataRow = dt.NewRow

                R("ReceivingNoId") = Val(row.Cells("ReceivingNoId").Value.ToString)
                R("Vendor") = row.Cells("Vendor").Value
                R("ReceivingNo") = row.Cells("ReceivingNo").Value
                R("ReceivingDate") = row.Cells("ReceivingDate").Value
                R("IGPNo") = row.Cells("IGPNo").Value
                R("Remarks") = row.Cells("Remarks").Value

                R("DcNo") = row.Cells("DcNo").Value
                R("Vehicle") = row.Cells("Vehicle").Value
                R("CostCenterName") = row.Cells("CostCenterName").Value

                R("VendorId") = Val(row.Cells("VendorId").Value.ToString)

                R("CostCenterID") = Val(row.Cells("CostCenterID").Value.ToString)

                R("Driver_Name") = row.Cells("Driver_Name").Value
                R("CurrencyType") = Val(row.Cells("CurrencyType").Value.ToString)
                R("CurrencyRate") = Val(row.Cells("CurrencyRate").Value.ToString)
                R("Transportation_Vendor") = Val(row.Cells("Transportation_Vendor").Value.ToString)
                R("Custom_Vendor") = Val(row.Cells("Custom_Vendor").Value.ToString)
                R("Arrival_Time") = row.Cells("Arrival_Time").Value
                R("Departure_Time") = row.Cells("Departure_Time").Value

                R("InvoiceNo") = row.Cells("InvoiceNo").Value

                dt.Rows.Add(R)

            Next

            If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 And Me.cmbReceivingNo.ActiveRow.Cells(0).Value <= 0 Then

                If dt.Rows.Count > 1 AndAlso flgMultiGRN = "False" Then

                    ShowErrorMessage("Related Configration against multi GRN is off")

                Else

                    If dt.Rows.Count <= 0 Then

                        ShowErrorMessage("You did not checked any row in the grid")

                    Else

                        Me.ReceivingNoId = cmbReceivingNo.Value

                        'If Me.ReceivingNoId > 0 AndAlso flgMultiGRN = "False" Then

                        frmPurchaseNew.fillPurchaseNewGrid(dt, Me.ReceivingNoId)

                        ' ElseIf flgMultiGRN = "True" Then

                        'frmPurchaseNew.fillPurchaseNewGrid(dt, Me.ReceivingNoId)

                        'Else

                        'ShowErrorMessage("Please Select ReceivingNo")

                        'End If

                        Me.Close()

                    End If

                End If

            Else

            If dt.Rows.Count <= 0 Then

                ShowErrorMessage("You did not checked any row in the grid")

            Else

                Me.ReceivingNoId = cmbReceivingNo.Value

                'If Me.ReceivingNoId > 0 Then

                frmPurchaseNew.fillPurchaseNewGrid(dt, Me.ReceivingNoId)

                'Else

                'ShowErrorMessage("Please Select ReceivingNo")

                'End If

                Me.Close()

            End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            Me.grdItems_KeyDown(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_InitializeLayout(sender As Object, e As Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbVendor.InitializeLayout

    End Sub
End Class