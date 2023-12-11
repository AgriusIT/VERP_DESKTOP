Public Class frmRptGrdProductionDetail
    Public FromDate As Date
    Public ToDate As Date
    Public ProjectId As Integer
    Public ProduceItem As Integer
    Private Sub frmRptGrdProductionDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Me.dtpFromDate.Value = Me.dtpFromDate.Value.Date.AddMonths(-1)
            'FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name as CostCenter From tblDefCostCenter")
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetAllRecords(Optional ByVal Condition As String = "")
        Try

            Dim str As String = String.Empty
            str = "SP_Production_Detail '" & FromDate.Date.ToString("yyyy-M-d 00:00:00") & "', '" & ToDate.Date.ToString("yyyy-M-d 23:59:59") & "', " & ProjectId & ", " & ProduceItem & ""
            Dim dt As New DataTable
            dt = GetDataTable(str)
            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns("ArticleId").Visible = False
            Me.GridEX1.RootTable.Columns("CostCenterID").Visible = False
            Me.GridEX1.RootTable.Columns("ArticleSizeName").Caption = "Size"
            Me.GridEX1.RootTable.Columns("ArticleColorName").Caption = "Color"
            Me.GridEX1.RootTable.Columns("ArticleUnitName").Caption = "Unit"
            Me.GridEX1.RootTable.Columns("Production").Caption = "Consume"
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        'If Me.cmbCostCenter.SelectedIndex = 0 Then
    '        '    ShowErrorMessage("Please select project")
    '        '    Me.cmbCostCenter.Focus()
    '        '    Exit Sub
    '        'End If
    '        GetAllRecords()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        Try
            IssuanceHistoryByProduction.ArticleId = Me.GridEX1.GetRow.Cells("ArticleId").Value
            IssuanceHistoryByProduction.CostCenterId = ProjectId
            IssuanceHistoryByProduction.startDate = FromDate.Date.ToString("yyyy-M-d 00:00:00")
            IssuanceHistoryByProduction.endDate = ToDate.Date.ToString("yyyy-M-d 23:59:59")
            ApplyStyleSheet(IssuanceHistoryByProduction)
            IssuanceHistoryByProduction.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Production Detail Table"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class