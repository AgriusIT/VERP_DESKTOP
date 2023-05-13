Imports SBModel

Public Class frmGrdRptClosingStockByGRNnDC

    Private Sub tsbDiplay_Click(sender As Object, e As EventArgs)
        GetClossingStocByGRNnDC()
    End Sub
    Public Sub GetClossingStocByGRNnDC()
        Try
            Dim table As DataTable = New DataTable()
            table = GetDataTable("SP_ClosingStockByGRNnDC")
            Me.grdCSO.DataSource = table
            Me.grdCSO.RetrieveStructure()
            Me.grdCSO.RootTable.Columns("ArticleId").Visible = False
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
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
    Private Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            Me.grdCSO.RootTable.Columns("Current Stock").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCSO.RootTable.Columns("GRN Stock").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCSO.RootTable.Columns("DC Stock").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCSO.RootTable.Columns("Estimated Stock").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdCSO.RootTable.Columns("Current Stock").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCSO.RootTable.Columns("GRN Stock").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCSO.RootTable.Columns("DC Stock").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCSO.RootTable.Columns("Estimated Stock").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdCSO.RootTable.Columns("Current Stock").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCSO.RootTable.Columns("GRN Stock").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCSO.RootTable.Columns("DC Stock").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCSO.RootTable.Columns("Estimated Stock").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdCSO.RootTable.Columns("Current Stock").FormatString = "N" & DecimalPointInQty
            Me.grdCSO.RootTable.Columns("GRN Stock").FormatString = "N" & DecimalPointInQty
            Me.grdCSO.RootTable.Columns("DC Stock").FormatString = "N" & DecimalPointInQty
            Me.grdCSO.RootTable.Columns("Estimated Stock").FormatString = "N" & DecimalPointInQty

            Me.grdCSO.RootTable.Columns("Current Stock").TotalFormatString = "N" & DecimalPointInQty
            Me.grdCSO.RootTable.Columns("GRN Stock").TotalFormatString = "N" & DecimalPointInQty
            Me.grdCSO.RootTable.Columns("DC Stock").TotalFormatString = "N" & DecimalPointInQty
            Me.grdCSO.RootTable.Columns("Estimated Stock").TotalFormatString = "N" & DecimalPointInQty

            Me.grdCSO.RootTable.Columns("ArticleId").Visible = False
            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = String.Empty
            CtrlGrdBar1.Email.Subject = "Estimated Stock"
            CtrlGrdBar1.Email.DocumentNo = "Estimated Stock By GRN and DC"
            CtrlGrdBar1.Email.DocumentDate = Date.Now
            Me.grdCSO.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            GetClossingStocByGRNnDC()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptClosingStockByGRNnDC_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim lbl As New Label
        lbl.Text = "Loading Please Wait...."
        lbl.Dock = DockStyle.Fill
        Me.Controls.Add(lbl)
        lbl.BringToFront()
        Application.DoEvents()
        GetSecurityRights()

        Try
            GetClossingStocByGRNnDC()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdCSO.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdCSO.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grdCSO.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Closing Stock by GRN and DC"
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class