''TASK: TFS1083 New Material Estimation screen to load  old cost sheet and then estimation should be loaded to old store issuance. Done by Ameen 23-07-2017
'' TASK : TFS1438 Muhammad Ameen : Minus quantity condition restricted to tag wise rather than department wise. On 20-09-2017
Imports System.Windows.Forms

Public Class frmAddSub
    Public RawMaterialId As String = ""
    Public PlanItemId As String = ""
    Public IsAddition As Boolean = False
    Public IsSubtraction As Boolean = False
    Public Grid As New Janus.Windows.GridEX.GridEX
    Public IsMEPrior As Boolean = False
    Public CheckQty As Double = 0
    Public TotalMinusQty As Double = 0
    Public TotalPlusQty As Double = 0
    Public PendingEstQty As Double = 0
    Public TotalMinusEstQty As Double = 0
    Public TotalPlusEstQty As Double = 0
    Public TotalNoTypeQty As Double = 0
    Public Type As String = String.Empty
    Private Const ADDITION_TYPE As String = "Plus"
    Private Const SUBTRACTION_TYPE As String = "Minus"
    Public DepartmentId As Integer = 0
    Public Department As String = String.Empty

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'If Type = True Then
            If Val(txtQty.Text) <= 0 Then
                msg_Error("Zero or less than zero is not allowed.")
                Me.txtQty.Text = ""
                Me.txtQty.Focus()
                Exit Sub
            End If
            If Type = "New" Then
                frmPlanTicketStandard.AddToGrid(Val(txtQty.Text), Type, Me.cmbUltraRawMaterial.Value, Me.cmbUltraRawMaterial.Text, cmbUltraRawMaterial.ActiveRow.Cells("PurchasePrice").Value, DepartmentId, Department)
            Else
                frmPlanTicketStandard.AddToGrid(Val(txtQty.Text), Type)
            End If
            'Else
            'If Val(txtQty.Text) <= 0 Then
            '    msg_Error("Zero or less than zero is not allowed.")
            '    Me.txtQty.Text = ""
            '    Me.txtQty.Focus()
            '    Exit Sub
            'End If
            'If (TotalMinusEstQty + Val(txtQty.Text)) > (TotalPlusEstQty + TotalNoTypeQty) Then
            '    msg_Error("Quantity is greator than estimated Qty.")
            '    Me.txtQty.Text = ""
            '    Me.txtQty.Focus()
            '    Exit Sub
            'Else
            '    'If (Val(txtQty.Text) + TotalMinusEstQty) > (PendingEstQty + TotalPlusEstQty) Then
            '    If (Val(txtQty.Text) + TotalMinusEstQty) > ((TotalPlusEstQty + TotalNoTypeQty) - PendingEstQty) Then

            '        msg_Error("You can't reduce consumed quantity.")
            '        Me.txtQty.Text = ""
            '        Me.txtQty.Focus()
            '        Exit Sub
            '    End If
            '    frmMaterialEstimation.SubToGrid(Val(txtQty.Text))
            'End If
            'End If
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub frmDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim dtGrid As DataTable = CType(Grid.DataSource, DataTable)

            If Type = "New" Then
                Dim ArticleIds As String = String.Empty

                For Each _Row As DataRow In dtGrid.Rows
                    If ArticleIds = String.Empty Then
                        ArticleIds = " " & _Row.Item("MaterialArticleId").ToString & " "
                    Else
                        ArticleIds += ", " & _Row.Item("MaterialArticleId").ToString & " "
                    End If
                Next
                Dim Str As String = "Select ArticleId AS MaterialArticleId, ArticleDescription AS Material, IsNull(SalePrice, 0) AS SalePrice, IsNull(PurchasePrice, 0) AS PurchasePrice, IsNull(Cost_Price, 0) AS CostPrice From ArticleDefTable WHERE ArticleId NOT IN ( " & ArticleIds & " )"
                FillUltraDropDown(Me.cmbUltraRawMaterial, Str)

                If Me.cmbUltraRawMaterial.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbUltraRawMaterial.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If

            Else
                'Me.cmbRawMaterial.SelectedValue = RawMaterialId

                Dim view As DataView = dtGrid.AsDataView
                view.RowFilter = " MaterialArticleId = " & Val(RawMaterialId) & " "
                dtGrid = view.ToTable()

                If dtGrid.Rows.Count > 1 Then

                    For i As Integer = 1 To dtGrid.Rows.Count - 1

                        dtGrid.Rows(i).BeginEdit()
                        dtGrid.Rows(i).Delete()
                        dtGrid.Rows(i).EndEdit()

                    Next

                End If

                'Dim Rows() As DataRow = dtGrid.Select(" MaterialArticleId =" & Val(RawMaterialId) & "")
                Me.cmbUltraRawMaterial.DisplayMember = "Material"
                Me.cmbUltraRawMaterial.ValueMember = "MaterialArticleId"
                Me.cmbUltraRawMaterial.DataSource = dtGrid  'Rows.CopyToDataTable()
                Me.cmbUltraRawMaterial.Value = Me.RawMaterialId
                Me.txtQty.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub Save()
    '    Dim connection As New OleDb.OleDbConnection
    '    Dim command As New OleDb.OleDbCommand
    '    connection = Con
    '    If connection.State = ConnectionState.Open Then connection.Close()
    '    connection.Open()
    '    Dim trans As OleDb.OleDbTransaction = connection.BeginTransaction
    '    Try
    '        command.CommandType = CommandType.Text
    '        command.Connection = connection
    '        command.Transaction = trans

    '        command.CommandText = "Insert INTO "

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
