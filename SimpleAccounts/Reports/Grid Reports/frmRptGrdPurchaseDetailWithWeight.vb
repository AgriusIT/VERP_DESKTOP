Public Class frmRptGrdPurchaseDetailWithWeight
    Enum GrdEnum
        ReceivingDate
        ReceivingNo
        Detail_Code
        Detail_Title
        ArticleCode
        ArticleDescription
        ArticleColorName
        ArticleSizeName
        ArticleUnitName
        ArticleSize
        Sz1
        Sz7
        Packing
        Qty
        Weight
        TotalWeight
    End Enum

    Private Sub frmRptGrdPurchaseDetailWithWeight_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmRptGrdPurchaseDetailWithWeight_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim Str As String
            Me.cmbPeriod.Text = "Current Month"
            ''Start TFS2124
            Str = "Select coa_detail_id, detail_title From vwCOADETAIL WHERE detail_title is not null "
            If getConfigValueByType("Show Customer On Purchase") = "True" Then
                Str += " AND Account_Type in ('Vendor','Customer') "
            Else
                Str += " AND  Account_Type='Vendor' "
            End If
            Str += " ORDER BY detail_title asc"
            ''End TFS2124
            FillDropDown(Me.cmbVendor, Str)
            FillDropDown(Me.cmbType, "Select ArticleTypeId, ArticleTypeName From ArticleTypeDefTable")
            FillGrid()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillGrid()
        Try
            'TFS3477: 11/June/2018: Waqar Raza: Added todays Last hours like (23:59:59) to remove the error of loading datawise
            Me.Cursor = Cursors.WaitCursor
            Dim str As String = String.Empty
            str = "SELECT dbo.ReceivingMasterTable.ReceivingDate, dbo.ReceivingMasterTable.ReceivingNo, " _
                 & "     dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription,  " _
                 & "     dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, dbo.ReceivingDetailTable.ArticleSize,  " _
                 & "     SUM(ISNULL(dbo.ReceivingDetailTable.Sz1, 0)) AS Sz1, SUM(ISNULL(dbo.ReceivingDetailTable.Sz7, 0)) AS Sz7,  " _
                 & "     SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0)) AS Qty, SUM(ISNULL(dbo.ArticleDefWeight.Weight, 0)) AS Weight, " _
                 & "     SUM(ROUND(ISNULL(dbo.ReceivingDetailTable.Qty, 0) * ISNULL(dbo.ArticleDefWeight.Weight, 0), 2)) AS TotalWeight " _
                 & "     FROM dbo.ArticleDefWeight RIGHT OUTER JOIN " _
                 & "     dbo.ArticleDefView INNER JOIN " _
                 & "     dbo.vwCOADetail INNER JOIN " _
                 & "     dbo.ReceivingMasterTable ON dbo.vwCOADetail.coa_detail_id = dbo.ReceivingMasterTable.VendorId INNER JOIN " _
                 & "     dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId ON  " _
                 & "     dbo.ArticleDefView.ArticleId = dbo.ReceivingDetailTable.ArticleDefId ON dbo.ArticleDefWeight.ArticleDefId = dbo.ArticleDefView.ArticleId " _
                 & "     WHERE     (dbo.ReceivingMasterTable.ReceivingDate BETWEEN CONVERT(DATETIME, '" & Me.dtpFromDate.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND CONVERT(DATETIME,  " _
                 & "     '" & Me.dtpToDate.Value.Date.ToString("dd/MMM/yyyy 23:59:59") & "', 102))  "
            str += IIf(Me.cmbVendor.SelectedIndex > 0, " AND vwCOADetail.coa_detail_id=" & Me.cmbVendor.SelectedValue & "", "")
            str += IIf(Me.cmbType.SelectedIndex > 0, " AND ArticleDefView.ArticleTypeId=" & Me.cmbType.SelectedValue & "", "")
            str += "     GROUP BY dbo.ReceivingMasterTable.ReceivingDate, dbo.ReceivingMasterTable.ReceivingNo, dbo.vwCOADetail.detail_code, " _
                 & "     dbo.vwCOADetail.detail_title, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  " _
                 & "     dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, dbo.ReceivingDetailTable.ArticleSize  " _
                 & "     ORDER BY dbo.ReceivingMasterTable.ReceivingNo "
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            dt.Columns.Add(GrdEnum.Packing.ToString, GetType(System.String))
            dt.Columns(GrdEnum.Packing.ToString).Expression = "(Sz1 + 'X' + Sz7)"

            If dt Is Nothing Then Exit Sub
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dt
            ' Me.grd.RetrieveStructure()
            Me.grd.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbVendor.SelectedValue
            FillDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title From vwCOADETAIL WHERE detail_title is not null AND Account_Type='Vendor' ORDER BY detail_title asc")
            Me.cmbVendor.SelectedValue = id
            id = Me.cmbType.SelectedValue
            FillDropDown(Me.cmbType, "Select ArticleTypeId, ArticleTypeName From ArticleTypeDefTable")
            Me.cmbType.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbVendor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.SelectedIndexChanged, cmbType.SelectedIndexChanged
        Try
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub dtpFromDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFromDate.ValueChanged, dtpToDate.ValueChanged
        Try
            If Me.dtpToDate.Value < Me.dtpFromDate.Value Then
                ShowErrorMessage("Error! To date less than from date")
                Me.btnGenerate.Enabled = False
                Me.dtpToDate.Focus()
                Exit Sub
            End If
            If Me.dtpFromDate.Value > Me.dtpToDate.Value Then
                ShowErrorMessage("Error! from date grater than to date")
                Me.btnGenerate.Enabled = False
                Me.dtpToDate.Focus()
                Exit Sub
            End If
            Me.btnGenerate.Enabled = True
            cmbVendor_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = " " & IIf(Me.cmbVendor.SelectedIndex > 0, Me.cmbVendor.Text & IIf(Me.cmbType.SelectedIndex > 0, "(" + Me.cmbType.Text.ToString.Replace("'", "''") + ")", ""), "") & " From " & Me.dtpFromDate.Value.Date.ToString("dd-MM-yyyy") & " To " & Me.dtpToDate.Value.Date.ToString("dd-MM-yyyy") + vbCrLf + "Purchase Detail Weight Report " & " "
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Purchase Detail With Weight" & Chr(10) & "From Date: " & dtpFromDate.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpToDate.Value.ToString("dd-MM-yyyy").ToString & ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFromDate.Value = Date.Today
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-1)
            Me.dtpToDate.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpToDate.Value = Date.Today
        End If
    End Sub
End Class