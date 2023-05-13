Imports SBModel
Public Class StoreIssuanceSummary
    Private Sub frmRptEnhancementNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            FillComboBox("costCenter")
            FillComboBox("ArticleType")
            Me.cmbPeriod.Text = "Current Month"
            cmbCostCenter.Visible = True
            Me.lblArticleType.Enabled = True
            Me.cmbArticleType.Enabled = True
            Me.dtpFrom.Visible = True
            Me.dtpTo.Visible = True
            Me.Label1.Visible = True
            Me.Label2.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub CallShowReport()
        Try
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("RptStoreIssuence_Summary", "{sp_Store_Issuence_Summary.ArticleDescription} <> '' " & IIf(cmbCostCenter.SelectedIndex > 0, " AND {sp_Store_Issuence_Summary.costCenter} = " & Me.cmbCostCenter.SelectedValue & "", "") & "" & IIf(cmbArticleType.SelectedIndex > 0, " AND {sp_Store_Issuence_Summary.ArticleTypeId} = " & Me.cmbArticleType.SelectedValue & "", "") & "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        GetCrystalReportRights()
        Me.CallShowReport()
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.UiButton2.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.UiButton2.Enabled = False
                    Exit Sub
                End If
            Else
                Me.UiButton2.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "Print" Then
                        Me.UiButton2.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged, cmbCostCenter.SelectedIndexChanged
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

    End Sub
    Public Sub FillComboBox(Optional ByVal Condition As String = "")
        Try
            If Condition = "costCenter" Then
                FillDropDown(cmbCostCenter, "SELECT CostCenterID, Name FROM  dbo.tblDefCostCenter")
            ElseIf Condition = "ArticleType" Then
                FillDropDown(cmbArticleType, "Select ArticleTypeId, ArticleTypeName  From ArticleTypeDefTable Order By 1 ASC")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class