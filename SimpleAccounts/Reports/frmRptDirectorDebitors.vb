Public Class frmRptDirectorDebitors

    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "Director" Then
                FillListBox(lstDirector.ListItem, "Select DISTINCT IsNull(Director,0) as DirectorId, Employee_Name as  DirectorName, Employee_Code From tblCustomer INNER JOIN tblDefEmployee ON tblDefEmployee.Employee_Id = tblCustomer.Director ORDER BY 2 ASC")
            ElseIf Condition = "Manager" Then
                FillListBox(lstManager.ListItem, "Select DISTINCT IsNull(Manager,0) as ManagerId, Employee_Name  as ManagerName, Employee_Code From tblCustomer INNER JOIN tblDefEmployee ON tblDefEmployee.Employee_Id = tblCustomer.Manager ORDER BY 2 ASC")
            ElseIf Condition = "SaleMan" Then
                FillListBox(lstSaleman.ListItem, "Select DISTINCT IsNull(SaleMan,0) as SaleManId, Employee_Name  as SalesMan, Employee_Code From tblCustomer INNER JOIN tblDefEmployee ON tblDefEmployee.Employee_Id = tblCustomer.SaleMan ORDER BY 2 ASC")
            ElseIf Condition = "Region" Then
                FillListBox(lstRegion.ListItem, "Select StateId, StateName From tblListState Order By StateName ASC")
            ElseIf Condition = "City" Then
                FillListBox(lstCity.ListItem, "Select CityId, CityName From tblListCity WHERE StateId In (" & Me.lstRegion.SelectedIDs & ") ORDER BY CityName ASC")
            ElseIf Condition = "Area" Then
                FillListBox(lstArea.ListItem, "Select TerritoryId, TerritoryName From tblListTerritory WHERE CityId IN (" & lstCity.SelectedIDs & ") ORDER BY TerritoryName ASC")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lstRegion_SelectedIndexChaned(ByVal sender As System.Object, ByVal e As SimpleAccounts.IndexEventArgs) Handles lstRegion.SelectedIndexChaned
        Try
            If Me.lstRegion.SelectedIDs.Length > 0 Then FillCombo("City")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstCity_SelectedIndexChaned(ByVal sender As System.Object, ByVal e As SimpleAccounts.IndexEventArgs) Handles lstCity.SelectedIndexChaned
        Try
            If Me.lstCity.SelectedIDs.Length > 0 Then FillCombo("Area")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmRptDirectorDebitors_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim lbl As New Label
        lbl.Visible = True
        lbl.Name = "Heading"
        lbl.Text = "Loading please wait..."
        lbl.AutoSize = False
        lbl.Dock = DockStyle.Fill
        Me.Controls.Add(lbl)
        lbl.BringToFront()
        Try


            FillCombo("Director")
            FillCombo("Manager")
            FillCombo("SaleMan")
            FillCombo("Region")
            FillCombo("City")
            FillCombo("Area")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            FillCombo("Director")
            FillCombo("Manager")
            FillCombo("SaleMan")
            FillCombo("Region")
            FillCombo("City")
            FillCombo("Area")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            GetCrystalReportRights()
            Dim strFilter As String = String.Empty
            strFilter = " {SP_DirectorDebtors;1.Amount} <> 0"
            If Me.lstDirector.SelectedIDs.Length > 0 Then
                strFilter += " AND {SP_DirectorDebtors;1.Director} IN [" & Me.lstDirector.SelectedIDs & "]"
            End If
            If Me.lstManager.SelectedIDs.Length > 0 Then
                strFilter += " AND {SP_DirectorDebtors;1.Manager} IN [" & Me.lstManager.SelectedIDs & "]"
            End If
            If Me.lstSaleman.SelectedIDs.Length > 0 Then
                strFilter += " AND {SP_DirectorDebtors;1.SaleMan} IN [" & Me.lstSaleman.SelectedIDs & "]"
            End If
            If Me.lstRegion.SelectedIDs.Length > 0 Then
                strFilter += " AND {SP_DirectorDebtors;1.StateId} IN [" & Me.lstRegion.SelectedIDs & "]"
            End If
            If Me.lstCity.SelectedIDs.Length > 0 Then
                strFilter += " AND {SP_DirectorDebtors;1.CityId} IN [" & Me.lstCity.SelectedIDs & "]"
            End If
            If Me.lstArea.SelectedIDs.Length > 0 Then
                strFilter += " AND {SP_DirectorDebtors;1.TerritoryId} IN [" & Me.lstArea.SelectedIDs & "]"
            End If

            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 00:00:00"))
            ShowReport("rptDirectorDebitors", strFilter.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class