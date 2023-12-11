Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.IO
Imports SBUtility.Utility
Imports System.Data ''TFS3764
Imports System.Data.SqlClient ''TFS3764
Imports Neodynamic.SDK.Barcode ''TFS3764
Public Class frmLeadCustomerDetailReport
    Implements IGeneral
    Public dv As New DataView
    Public Shared dt As DataTable
    Public LeadProfileId As Integer = 0
    Public ControlName As New Form
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Public DoHavePrintRights As Boolean = False
    Public DoHaveExportRights As Boolean = False
    Public DoHaveFieldChooserRights As Boolean = False
    Public DoHaveActivityHistoryRights As Boolean = False
    Public DoHaveConvertToAccountRights As Boolean = False
    Public DoHaveGetAllRights As Boolean = False
    Dim leadProfileDAL As New LeadProfileDAL2()
    Public TEST As Integer

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
            Customer.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True
                ElseIf RightsDt.FormControlName = "Customer" Then
                    Customer.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strQuery As String = String.Empty
            strQuery = "SP_GETCustomerDetailReport"
            dt = GetDataTable(strQuery)
            dt.TableName = "Item"
            dv.Table = dt
            Me.grd.DataSource = dv.ToTable
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Email.TextChanged
        Try

            If dv IsNot Nothing Then
                If Me.Email.Text <> String.Empty Then
                    dv.RowFilter = "Email1 LIKE '%" & Me.Email.Text & "%' or  Email2 LIKE '%" & Me.Email.Text & "%' "
                    Me.grd.DataSource = dv
                Else
                    Me.grd.DataSource = dt
                End If
            End If
            'If grd.RowCount > 0 Then
            '    'lblItemName.Text = grd.CurrentRow.Cells("Item_Name").Value.ToString
            'End If
            'If txtSearch.Text = "" Then
            '    lblItemName.Text = ""
            'End If
            ' Me.uitxtItemName.Text = txtSearch.Text.ToString  ''TFS3764
            'Me.grd.AutoSizeColumns()
            'ArticleId = grd.CurrentRow.Cells("ArticleId").Value.ToString
            'Qty = Val(txtQty.Text)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
    End Function
    Private Sub GrdCostCenter_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Lead Profile"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ShowDataGrid_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            GetAllRecords()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try
            frmLeadProfileList2._tEST = Me.grd.GetRow.Cells("LeadProfileId").Value
            frmMain.LoadControl("frmLeadProfileList2")
            frmLeadProfileList2.Getbyid()
            ''frmLeadProfileList2.frmLeadProfileList2_Load(Nothing, Nothing)
            ''frmLeadProfileList2.grdLeadProfileList.DataSource = leadProfileDAL.GetById(frmLeadProfileList2._tEST)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class