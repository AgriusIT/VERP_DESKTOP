Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports SBUtility.Utility


Public Class frmPendingCustomerList
    Implements IGeneral
    Public CustomerId As Integer = 0

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

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

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid(Optional ByVal condition As String = "")
        Try
            Dim dt As New DataTable
            Dim str As String = String.Empty
            str = "SELECT coa_Detail_Id, detail_Code as Code,Detail_Title as Customer,Territoryname as Territory,cityname as City,'' as [Get Order] FROM vwCOADetail WHERE ACCOUNT_TYPE='CUSTOMER' AND COA_DETAIL_ID NOT IN(SELECT VendorId FROM SalesOrderMasterTable WHERE  (CONVERT(varchar, dbo.SalesOrderMasterTable.SalesOrderDate, 102)) = CONVERT(datetime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "', 102))"
            dt = GetDataTable(str)
            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            GridEX1.RootTable.Columns("Get Order").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            GridEX1.RootTable.Columns("Get Order").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            GridEX1.RootTable.Columns("Get Order").ButtonText = "Get Order"
            GridEX1.RootTable.Columns("coa_Detail_Id").Visible = False
            Me.GridEX1.AutoSizeColumns()
            'Dim btn As New DataGridViewButtonColumn()
            ''Me.GridEX1.columns.Add(btn)
            'GridEX1.RootTable.Columns().Add()
            'btn.HeaderText = "Click Data"
            'btn.Text = "Click Here"
            'btn.Name = "btn"
            'btn.UseColumnTextForButtonValue = True


        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmPendingCustomerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Try
            If e.Column.Key = "Get Order" Then
                CustomerId = Me.GridEX1.GetRow.Cells("coa_detail_id").Value
                DialogResult = Windows.Forms.DialogResult.Yes
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class