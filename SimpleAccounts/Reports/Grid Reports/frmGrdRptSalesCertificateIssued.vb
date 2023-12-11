''12-June-2014 TASK:2680 Imran Ali Sales Certificate Issued Report

Public Class frmGrdRptSalesCertificateIssued

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid(Optional ByVal Condition As String = "")
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT SC.SaleCertificateId, SC.SaleCertificateNo as [Certificate No], SC.SaleCertificateDate as [Certificate Date], SC.DeliveredTo as [Buyer's Name], SC.Engine_No, SC.Chassis_No, SC.ModelCode , SC.Model_Desc as [Model Description], COA.detail_code as [Account Code], COA.detail_title as [Dealer], " _
                     & " COA.CityName as [City], COA.TerritoryName as [Territory] " _
                     & " FROM dbo.SalesCertificateTable AS SC INNER JOIN " _
                     & " dbo.SalesMasterTable AS SM ON SC.SalesId = SM.SalesId INNER JOIN " _
                     & " dbo.vwCOADetail AS COA ON SM.CustomerCode = COA.coa_detail_id "
            strSQL += " WHERE SC.SaleCertificateNo <> '' AND (Convert(Varchar, SC.SaleCertificateDate, 102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "

            If Me.cmbVendor.Value > 0 Then
                strSQL += "AND SM.CustomerCode =" & Me.cmbVendor.Value & ""
            End If
            Dim objDt As New DataTable
            objDt = GetDataTable(strSQL)
            Me.grdSaved.DataSource = objDt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("SaleCertificateId").Visible = False
            Me.grdSaved.RootTable.Columns("Certificate No").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("Certificate Date").FormatString = "dd/MMM/yyyy"
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdSaved.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            Dim str As String = String.Empty
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                              "dbo.tblListTerritory.TerritoryName as Territory, tblCustomer.Email, tblCustomer.Phone " & _
                              "FROM  dbo.tblCustomer INNER JOIN " & _
                              "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                              "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                              "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                              "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                              "WHERE 1=1 "
            'If flgCompanyRights = True Then
            '    str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            'End If
            'If IsEditMode = False Then
            '    str += " and vwCOADetail.Active=1"
            'Else
            '    str += " and vwCOADetail.Active in(0,1,NULL)"
            'End If
            ''Start TFS2124
            If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                str += " AND (dbo.vwCOADetail.account_type IN('Customer','Customer Service'))  "
            Else
                str += " AND (dbo.vwCOADetail.account_type in('Customer','Vendor'))  "
            End If
            ''End TFS2124
            str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title"
            FillUltraDropDown(cmbVendor, str)
            Me.cmbVendor.Rows(0).Activate()
            If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmGrdRptSalesCertificateIssued_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            Me.DateTimePicker1.Value = Now.AddMonths(-1)
            Me.DateTimePicker2.Value = Now
            FillCombos()
            FillGrid()
            Me.cmbVendor.Focus()
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If e.Column.Key = "Certificate No" Then
                ''Below line is commented on 01-08-18 against TASK TFS4044. Done by Amin
                'frmSalesCertificate.Tag = Val(Me.grdSaved.GetRow.Cells("SaleCertificateId").Value.ToString)
                frmMain.LoadControl("frmSalesCertificate")
                frmSalesCertificate.EditRecord(Val(Me.grdSaved.GetRow.Cells("SaleCertificateId").Value.ToString))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = "Issued Sales Certificate Report" & Chr(10) & CompanyTitle & Chr(10) & IIf(Me.cmbVendor.Value > 0, Me.cmbVendor.Text, "All") & Chr(10) & " From: " & Me.DateTimePicker1.Value.Date.ToString("dd/MMM/yyyy") & " To: " & Me.DateTimePicker2.Value.Date.ToString("dd/MMM/yyyy")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0I
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos()
            Me.cmbVendor.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class