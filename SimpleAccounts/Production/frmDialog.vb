''TASK: TFS1083 New Material Estimation screen to load  old cost sheet and then estimation should be loaded to old store issuance. Done by Ameen 23-07-2017
'' TASK : TFS1438 Muhammad Ameen : Minus quantity condition restricted to tag wise rather than department wise. On 20-09-2017
Imports System.Windows.Forms

Public Class frmDialog
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


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsMEPrior = False Then
                If IsAddition = True Then
                    ''TASK:TFS1141 Validate minus quantity
                    If Val(txtQty.Text) <= 0 Then
                        msg_Error("Zero or less than zero is not allowed.")
                        Me.txtQty.Text = ""
                        Me.txtQty.Focus()
                        Exit Sub
                    End If
                    frmMaterialEstimation.AddToGrid(Val(txtQty.Text))
                Else
                    'frmMaterialEstimation.SubtractFromGrid(Val(txtQty.Text), 0, 0)
                    If Val(txtQty.Text) <= 0 Then
                        msg_Error("Zero or less than zero is not allowed.")
                        Me.txtQty.Text = ""
                        Me.txtQty.Focus()
                        Exit Sub
                    End If
                    ''Below row is commented against TASK TFS1438
                    'If (Val(txtQty.Text) + TotalMinusQty) > TotalPlusQty Then
                    ''TASK TFS1438
                    If (TotalMinusEstQty + Val(txtQty.Text)) > (TotalPlusEstQty + TotalNoTypeQty) Then
                        msg_Error("Quantity is greator than estimated Qty.")
                        Me.txtQty.Text = ""
                        Me.txtQty.Focus()
                        Exit Sub
                    Else
                        'If (Val(txtQty.Text) + TotalMinusEstQty) > (PendingEstQty + TotalPlusEstQty) Then
                        If (Val(txtQty.Text) + TotalMinusEstQty) > ((TotalPlusEstQty + TotalNoTypeQty) - PendingEstQty) Then

                            msg_Error("You can't reduce consumed quantity.")
                            Me.txtQty.Text = ""
                            Me.txtQty.Focus()
                            Exit Sub
                        End If
                        frmMaterialEstimation.SubToGrid(Val(txtQty.Text))
                    End If
                    ''END TASK TFS1438
                    ''END TASK:TFS1141
                End If
                ''TASK TFS1083 Added New Estimation Link to load old Cost Sheet.
            Else
                If IsAddition = True Then
                    frmMaterialEstimationPrior.AddToGrid(Val(txtQty.Text))
                Else
                    'frmMaterialEstimation.SubtractFromGrid(Val(txtQty.Text), 0, 0)
                    frmMaterialEstimationPrior.SubtractFromGrid(Val(txtQty.Text))
                End If
            End If
            ''END TASK: TFS1083
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
            ''TAKS: TFS1083 to load according to Old Cost Sheet.
            Dim dtCombo As New DataTable
            Dim ConfigValue As String = getConfigValueByType("CostSheetType")
            If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then


                Dim Str As String = "Select ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription From ArticleDefTable Join tblCostSheet On ArticleDefTable.ArticleId = tblCostSheet.ArticleId Where tblCostSheet.ArticleId = " & RawMaterialId & " And tblCostSheet.MasterArticleID =" & PlanItemId & " "
                Dim dtGrid As DataTable = CType(Grid.DataSource, DataTable)
                Dim dr() As DataRow = dtGrid.Select("ArticleId = '" & RawMaterialId & "' And MasterArticleID ='" & PlanItemId & "'")

                dtCombo = dr.CopyToDataTable()
            Else
                ''END TAKS: TFS1083
                Dim Str As String = "Select ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription From ArticleDefTable Join tblCostSheet On ArticleDefTable.ArticleId = tblCostSheet.ArticleId Where tblCostSheet.ArticleId = " & RawMaterialId & " And tblCostSheet.ParentID =" & PlanItemId & " "
                Dim dtGrid As DataTable = CType(Grid.DataSource, DataTable)
                Dim dr() As DataRow = dtGrid.Select("ArticleId = '" & RawMaterialId & "' And ParentId ='" & PlanItemId & "'")

                dtCombo = dr.CopyToDataTable()

            End If
            Me.cmbRawMaterial.DisplayMember = "ArticleDescription"
            Me.cmbRawMaterial.ValueMember = "ArticleID"
            Me.cmbRawMaterial.DataSource = dtCombo
            'FillDropDown(Me.cmbRawMaterial, Str, False)
            Me.txtQty.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Save()
        Dim connection As New OleDb.OleDbConnection
        Dim command As New OleDb.OleDbCommand
        connection = Con
        If connection.State = ConnectionState.Open Then connection.Close()
        connection.Open()
        Dim trans As OleDb.OleDbTransaction = connection.BeginTransaction
        Try
            command.CommandType = CommandType.Text
            command.Connection = connection
            command.Transaction = trans

            command.CommandText = "Insert INTO "

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
