Imports SBModel
Public Class rptStockByLocation
    Public ObjCrystal As Object
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dt As New DataTable
            Dim adp As New OleDb.OleDbDataAdapter
            Dim dtData As New DataTable()
            adp = New OleDb.OleDbDataAdapter("SP_Rpt_Stock_ByLocation '" & Date.Now.Date.ToString("yyyy-M-d 00:00:00") & "'," & Me.ComboBox1.SelectedValue, Con)
            adp.Fill(dtData)
            Me.GridEX1.DataSource = dtData
            Me.GridEX1.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
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
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Grid Print" Then
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

    Private Sub rptStockByLocation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            lnkRefresh_LinkClicked(Nothing, Nothing)
        End If
    End Sub

    Private Sub rptStockByLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetSecurityRights()
        Dim Str As String = String.Empty
        Str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
        FillDropDown(Me.ComboBox1, Str, False)

    End Sub

    Private Sub GridEX1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.SelectionChanged
        Try
            Me.Text = "Stock Statment [" & GridEX1.GetRow.Cells("ArticleCode").Text & "]"
            If Me.SplitContainer1.Panel2Collapsed = True Then Exit Sub
            If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                frmhistory.Close()
            End If
            If GridEX1.RowCount = 0 Then
                Exit Sub
            End If
            frmhistory.article_code = GridEX1.GetRow.Cells("ArticleId").Text
            frmhistory.ArticleCode = GridEX1.GetRow.Cells("ArticleCode").Text
            frmhistory.startDate = CDate("2000-1-1 00:00:00")
            frmhistory.EndDate = Date.Now.Date
            frmhistory.LocationId = Me.ComboBox1.SelectedValue
            frmhistory.ArticleDescription = Me.GridEX1.GetRow.Cells(2).Text
            '  frmhistory.Show()
            frmhistory.TopLevel = False
            frmhistory.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            frmhistory.Dock = DockStyle.Fill
            Me.SplitContainer1.Panel2.Controls.Add(frmhistory)
            frmhistory.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        If Me.SplitContainer1.Panel2Collapsed = True Then
            Me.SplitContainer1.Panel2Collapsed = False
            GridEX1_SelectionChanged(GridEX1, Nothing)
        Else
            Me.SplitContainer1.Panel2Collapsed = True
            If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                frmhistory.Close()
            End If
        End If
    End Sub
    Private Sub cmdGenerateRptStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenerateRptStock.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dt As New DataTable
            Dim adp As New OleDb.OleDbDataAdapter
            Dim dtData As New DataTable()
            adp = New OleDb.OleDbDataAdapter("SP_Rpt_Stock_ByLocation '" & Date.Now.Date.ToString("dd-MMM-yyyy") & " 11:59:59'," & Me.ComboBox1.SelectedValue, Con)
            adp.Fill(dtData)
            'Dim rpt As New rptStock_ByLocation
            'Dim IsCompanyInfo As Boolean = Convert.ToBoolean(GetConfigValue("ShowCompanyAddressOnPageHeader").ToString)
            'Dim CompanyTitle As String = GetConfigValue("CompanyNameHeader").ToString()
            'Dim CompanyAddressHeader As String = GetConfigValue("CompanyAddressHeader").ToString()

            'rpt.SetParameterValue("@ToDate", Date.Now)
            'rpt.SetParameterValue("@LocationId", Me.ComboBox1.SelectedValue)
            'rpt.SetParameterValue("@CompanyName", CompanyTitle)
            'rpt.SetParameterValue("@CompanyAddress", CompanyAddressHeader)
            'rpt.SetParameterValue("@ShowHeader", IsCompanyInfo)
            'rpt.SetDataSource(dtData)
            'ObjCrystal = rpt
            'My.Forms.frmRptViewer_StockByLocation.Show()
            AddRptParam("@ToDate", Date.Now.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("@LocationId", Me.ComboBox1.SelectedValue)
            ShowReport("rptStock_ByLocation", , , , False, "Nothing", , dtData)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            CtrlGrdBar1_Load(CtrlGrdBar1, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkRefresh_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRefresh.LinkClicked
        Try

            Dim id As Integer = 0
            id = Me.ComboBox1.SelectedValue
            Dim Str As String = String.Empty
            Str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            FillDropDown(Me.ComboBox1, Str, False)
            Me.ComboBox1.SelectedValue = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = "Stock By Location (" & Me.ComboBox1.Text & ")"
        Catch ex As Exception

        End Try
    End Sub
End Class