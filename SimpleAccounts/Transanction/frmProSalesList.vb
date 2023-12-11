Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmProSalesList
    Implements IGeneral
    Dim Sales As PropertySalesBE
    Dim objDAL As PropertySalesDAL
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Dim IsFormLoaded As Boolean = False
    Private Sub frmProDealerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.btnAddDock.Select()
        btnAddDock.FlatAppearance.BorderSize = 0
        btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
        GetAllRecords()
        ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        CtrlGrdBar1_Load(Nothing, Nothing)
        IsFormLoaded = True
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns("PropertySalesId").Visible = False
            Me.grd.RootTable.Columns("BuyerAccountId").Visible = False

            '// Adding Delete Button in the grid
            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                DoHaveDeleteRights = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            objDAL = New PropertySalesDAL
            Dim dt As DataTable = objDAL.GetAll()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Get_All(ByVal SalesNo As String)
        Try
            Get_All = Nothing
            If Not SalesNo.Length > 0 Then Exit Try
            If IsFormLoaded = True Then
                IsDrillDown = True
                Dim flag As Boolean = False
                flag = Me.grd.Find(Me.grd.RootTable.Columns("VoucherNo"), Janus.Windows.GridEX.ConditionOperator.Equal, (SalesNo).Trim, 0, 1)
                Me.grd_DoubleClick(Nothing, Nothing)
                frmModProperty.Tags = ""
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function

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
    Private Sub btnAddDock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDock.Click
        Try
            frmProSales.PSID = -1
            frmProSales.EditAble = 0
            frmProSales.DoHaveSaveRights = DoHaveSaveRights
            frmProSales.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            If Me.grd.RowCount > 0 Then
                frmProSales.PSID = Val(Me.grd.CurrentRow.Cells("PropertySalesId").Value.ToString)
                frmProSales.DoHaveUpdateRights = DoHaveUpdateRights
            Else
                frmProSales.PSID = -1
            End If
            frmProSales.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            Sales = New PropertySalesBE
            Sales.SalesId = Val(Me.grd.CurrentRow.Cells("PropertysalesId").Value.ToString)
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If DoHaveDeleteRights = True Then
                    objDAL.Delete(Sales)
                    Me.grd.GetRow.Delete()
                    SaveActivityLog("Configuration", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
                Else
                    msg_Information("You do not have delete rights.")
                End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Purchase"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProSalesList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                frmPropertySearch.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class